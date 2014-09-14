using UnityEngine;
using System.Collections;

/// <summary>
///     Basic controller for side-scroller.
///     Manage inputs and apply velocity on CharacterController.
/// </summary>
public class Controller : MonoBehaviour
{
    /// <summary>
    ///     Does it reading inputs?
    /// </summary>
    public bool actif = true;

    /// <summary>
    ///     Which camera must be ref for moving
    /// </summary>
    public Camera camRef;

    /// <summary>
    ///     Character controller binded.
    /// </summary>
    public CharacterController cc;

    /// <summary>
    ///     Speed of horizontal and vertical moves.
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    ///     Speed for reorientation after rotation.
    /// </summary>
    public float rotationSpeed = 5.0f;

    /// <summary>
    ///     How many current velocity will be kept after frame.
    /// </summary>
    public float velocityKept = 0.1f;

    /// <summary>
    ///     Gravity force.
    /// </summary>
    public float gravity = 9.81f;

    /// <summary>
    ///     Current velocity.
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    ///     Does player send inputs?
    /// </summary>
    private bool userInputs = false;

    private void Start()
    {
        if (this.camRef == null)
            this.camRef = Camera.main;
        if (this.cc == null)
            this.GetComponent<CharacterController>();
        this.StartCoroutine("Move");
        this.StartCoroutine("Rotate");
    }

    /// <summary>
    ///     Check controller in camera's bounds (X only).
    /// </summary>
    /// <returns></returns>
    private bool CheckBorder()
    {
        Vector3 screenPos = this.camRef.WorldToScreenPoint(this.transform.position);
        Vector3 max = this.camRef.WorldToViewportPoint(this.collider.bounds.max + this.velocity * Time.deltaTime);
        Vector3 min = this.camRef.WorldToViewportPoint(this.collider.bounds.min + this.velocity * Time.deltaTime);

        if (max.x >= 0.95f || min.x < 0.05f)
            return (false);
        else
            return (true);
    }

    /// <summary>
    ///     Apply velocity and reset Y on controller grounded.
    /// </summary>
    private void FixedUpdate()
    {
        //Debug.Log("velocity " + this.velocity.x + " " + this.velocity.y + " " + this.velocity.z);
        if (this.CheckBorder() == true)
        this.cc.Move(this.velocity * Time.deltaTime);
        //this.cc.Move(Vector3.right);
        float previousY = this.velocity.y;

        this.velocity *= this.velocityKept;
        this.velocity.y = previousY;
        if (this.cc.isGrounded == false)
            this.velocity += (-Vector3.up * this.gravity) * Time.deltaTime;
        else if (this.velocity.y < 0.0f)
            this.velocity.y = 0.0f;

    }

    /// <summary>
    ///     Manage rotation and reorientation of controller.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Rotate()
    {
        while (true)
        {
            if (this.userInputs == false)
            {
                yield return (null);
                continue;
            }
            Vector3 normalizedVelo = this.velocity.normalized;
            //Debug.Log("angle = " + (Mathf.Atan2(this.transform.forward.z, this.transform.forward.x) - Mathf.Atan2(this.velocity.normalized.z, this.velocity.normalized.x)) * Mathf.Rad2Deg);
            //this.transform.Rotate(new Vector3(0.0f, Mathf.Atan2(this.transform.forward.z, this.transform.forward.x) - Mathf.Atan2(normalizedVelo.z, normalizedVelo.x) *
            this.transform.Rotate(new Vector3(0.0f, Mathf.Acos(Vector3.Dot(this.transform.forward, normalizedVelo)) *
                                                    Vector3.Cross(this.transform.forward, normalizedVelo).y *
                                                    Mathf.Rad2Deg * Time.deltaTime * this.rotationSpeed, 0.0f));
            yield return (null);
        }
    }

    /// <summary>
    ///     Manage user inputs to create a velocity.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move()
    {
        while (true)
        {
            Vector3 camForward = this.camRef.transform.forward;
            Vector3 move;

            camForward.y = 0.0f;
            camForward = camForward.normalized;
            move = ((Input.GetAxis("Horizontal") * this.camRef.transform.right) + (Input.GetAxis("Vertical") * camForward)).normalized;
            if (move == Vector3.zero)
                this.userInputs = false;
            else
                this.userInputs = true;
            if (this.userInputs == true)
                move = move * ((200.0f - Vector3.Angle(this.transform.forward, move)) * 0.005f); // 0.005 = 1 / 200
            this.velocity += move * this.moveSpeed;// *Time.deltaTime;
            yield return (null);
        }
    }

    /// <summary>
    ///     Accessor on velocity to adda force to it.
    /// </summary>
    /// <param name="force">
    ///     Force to add.
    /// </param>
    public void AddForce(Vector3 force)
    {
        //Debug.Log("previous velocity " + this.velocity);
        this.velocity += force;
        //Debug.Log("new velocity " + this.velocity);
    }
}
