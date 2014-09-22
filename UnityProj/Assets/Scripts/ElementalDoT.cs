using UnityEngine;
using System.Collections;

public class ElementalDoT : MonoBehaviour
{
    /// <summary>
    ///     Damage given.
    /// </summary>
    public float damage = -1.0f;

    /// <summary>
    ///     Damage frequency (seconds).
    /// </summary>
    public float frequency = 1.0f;

    /// <summary>
    ///     How long effect is active?
    /// </summary>
    public float time = 10.0f;

    /// <summary>
    ///     Element type.
    /// </summary>
    public eElements element;

    /// <summary>
    ///     Target.
    /// </summary>
    [HideInInspector]
    public Entity entity;

    private float _startTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        this._startTime = Time.time;
        InvokeRepeating("DealDamage", 0.0f, this.frequency);
	}
	
    public void DealDamage()
    {
        this.entity.DealDamage(this.damage);
        if ((Time.time - this._startTime) > this.time)
        {
            CancelInvoke("DealDamage");
            GameObject.Destroy(this.gameObject);
        }
    }

    public void ResetTimer()
    {
        this._startTime = Time.time;
    }
}
