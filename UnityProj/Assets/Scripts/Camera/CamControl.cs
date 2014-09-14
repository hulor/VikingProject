using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour
{
    /// <summary>
    ///     Path curve.
    /// </summary>
    public AnimationCurve pathCurveX;
    public AnimationCurve pathCurveY;
    public AnimationCurve pathCurveZ;

    /// <summary>
    ///     Rotation curve.
    /// </summary>
    public AnimationCurve rotationCurveX;
    public AnimationCurve rotationCurveY;
    public AnimationCurve rotationCurveZ;


    /// <summary>
    ///     Players group.
    /// </summary>
    public Transform[] players;

    /// <summary>
    ///     Last position of players' center.
    /// </summary>
    private Vector3 lastCenter = Vector3.zero;

    /// <summary>
    ///     Position on curves.
    /// </summary>
    private float animTime = 0.0f;

    private void Start()
    {
        this.lastCenter = this.ComputeCenter();
    }

    /// <summary>
    ///     Find players' center.
    /// </summary>
    /// <returns>
    ///     Players' center.
    /// </returns>
    private Vector3 ComputeCenter()
    {
        Vector3 center = Vector3.zero;

        for (int i = 0, size = this.players.Length; i < size; ++i)
        {
            center += this.players[i].position;
        }
        center /= this.players.Length;
        center.z = 0.0f;
        center.y = 0.0f;
        return (center);
    }

    /// <summary>
    ///     Does path curves have been computed?
    /// </summary>
    /// <returns></returns>
    private bool HasPathCurve()
    {
        return ((this.pathCurveX != null && this.pathCurveX.length != 0) &&
                (this.pathCurveY != null && this.pathCurveY.length != 0) &&
                (this.pathCurveZ != null && this.pathCurveZ.length != 0));
    }

    /// <summary>
    ///     Does rotation curves have been computed?
    /// </summary>
    /// <returns></returns>
    private bool HasRotationCurve()
    {
        return ((this.rotationCurveX != null && this.rotationCurveX.length != 0) &&
                (this.rotationCurveY != null && this.rotationCurveY.length != 0) &&
                (this.rotationCurveZ != null && this.rotationCurveZ.length != 0));
    }

    /// <summary>
    ///     Move camera on player center delta and curves (or right vector if no curves).
    /// </summary>
    private void Update()
    {
        Vector3 center = this.ComputeCenter();
        Vector3 delta = (center - this.lastCenter);
        float deltaTime = Vector3.Dot(delta, this.transform.right);

        this.animTime += deltaTime;
        if (this.HasPathCurve() == false)
            this.transform.position += (center - this.lastCenter);
        else
            this.transform.position = new Vector3(this.pathCurveX.Evaluate(this.animTime),
                                                  this.pathCurveY.Evaluate(this.animTime),
                                                  this.pathCurveZ.Evaluate(this.animTime));

        if (this.HasRotationCurve() == true)
            this.transform.rotation = Quaternion.Euler(new Vector3(this.rotationCurveX.Evaluate(this.animTime),
                                                                   this.rotationCurveY.Evaluate(this.animTime),
                                                                   this.rotationCurveZ.Evaluate(this.animTime)));
        this.lastCenter = center;
    }


#if UNITY_EDITOR
    /// <summary>
    ///     Draw followed center.
    /// </summary>
    private void OnDrawGizmos()
    {
        Color previous = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.lastCenter, 2.0f);
        Gizmos.color = previous;
    }
#endif
}
