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

    private void FixedUpdate()
    {
        if (this.controller.cc.isGrounded == true &&
            Input.GetButtonUp("Jump") == true)
        {
            this.controller.AddForce(Vector3.up * this.jumpForce);
        }
    }
}
