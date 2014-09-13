using UnityEngine;
using System.Collections;

/// <summary>
///     Manage entity life and death.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    ///     Handler for die event.
    /// </summary>
    public delegate void DieHandler();

    /// <summary>
    ///     Event thrown when entity dies.
    /// </summary>
    public event DieHandler dieEvent;

    /// <summary>
    ///     Max HP.
    /// </summary>
    public float hpTotal = 50.0f;
    
    /// <summary>
    ///     Does the entity is invincibile?
    /// </summary>
    public bool invincibility = false;

    /// <summary>
    ///     Current HP.
    /// </summary>
    private float _hp;

    private void Start()
    {
        this._hp = this.hpTotal;
    }

    /// <summary>
    ///     Public hp modificator.
    /// </summary>
    /// <param name="damage">
    ///     Hp gift to entity (can be positive or negative).
    /// </param>
    /// <returns>
    ///     Does entity is dead?
    /// </returns>
    public virtual bool DealDamage(float damage)
    {
        if (this.invincibility == true)
            return (false);
        this._hp = Mathf.Clamp(this._hp + damage, 0.0f, this.hpTotal);

        if (this._hp == 0.0f)
        {
            this.Kill();
            return (true);
        }
        else
            return (false);
    }

    public virtual void Kill()
    {
        //this.SendMessage("EntityDie", SendMessageOptions.DontRequireReceiver);
        if (this.dieEvent != null)
            this.dieEvent();
        GameObject.Destroy(this.gameObject);
    }
}
