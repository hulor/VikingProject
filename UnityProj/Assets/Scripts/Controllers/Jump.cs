using UnityEngine;
using System.Collections;

/// <summary>
///     Manage jump and add force to controller.
/// </summary>
public class Jump : MonoBehaviour
{
    /// <summary>
    ///     Controller on which force will be add.
    /// </summary>
    public Controller controller;

    /// <summary>
    ///     Jump force.
    /// </summary>
    public float jumpForce = 15.0f;

    /// <summary>
    ///     Collider binded to jumper object.
    /// </summary>
    public CapsuleCollider collider;

    /// <summary>
    ///     Max distance tolerated for designed as grounded.
    /// </summary>
    public float maxDistFromfloor = 0.01f;

    /// <summary>
    ///     Does the gameObject currently grounded?
    /// </summary>
    private bool _isGrounded = false;

    private void Start()
    {
        if (this.controller == null)
        {
            this.controller = this.GetComponent<Controller>();
            if (this.controller == null)
            {
                this.enabled = false;
                return ;
            }
        }
    }

    /// <summary>
    ///     Set _isGrounded according to -Up sphereCast.
    /// </summary>
    private void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.SphereCast(this.transform.position, collider.radius, -Vector3.up, out hit, this.collider.height * 0.5f + this.maxDistFromfloor) == true)
        {
            this._isGrounded = true;
        }
        else
        {
            this._isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        this.CheckGrounded();
        if (this._isGrounded == true &&
            Input.GetButtonUp("Jump") == true)
        {
            this.controller.AddForce(Vector3.up * this.jumpForce);
        }
    }
}
