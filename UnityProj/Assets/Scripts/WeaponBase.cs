using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    ///     How much weapon hurts.
    /// </summary>
    public float hit = -10.0f;

    /// <summary>
    ///     How much weapon hurts on huge attack.
    /// </summary>
    public float hitFuriousAttack = -20.0f;

    /// <summary>
    ///     Weapon attack speed.
    /// </summary>
    public float speedAttack = 1.0f;

    /// <summary>
    ///     Time to wait after a furious attack.
    /// </summary>
    public float speedFuriousAttack = 1.5f;

    /// <summary>
    ///     Weapon mesh.
    /// </summary>
    public GameObject mesh;

    /// <summary>
    ///     Entity carrying weapon.
    /// </summary>
    public ShrineManager shrineManager;

    /// <summary>
    ///     Time when last attack was thrown.
    /// </summary>
    private float _lastHitTime = 0.0f;

    private float _timeToWait = 0.0f;


    /// <summary>
    ///     Entity contained in collider.
    /// </summary>
    private List<Entity> _targets = new List<Entity>();


    private void Start()
    {
        this._lastHitTime = Time.time - this.speedAttack;
    }

    /// <summary>
    ///     Use weapon on an entity.
    /// </summary>
    /// <param name="furious">
    ///     Does the attack is a Furious attack?
    /// </param>
    public virtual void Attack(bool furious)
    {
        if (furious == true)
            this.HitEnemy(this.speedFuriousAttack, this.hitFuriousAttack);
        else
            this.HitEnemy(this.speedAttack, this.hit);
    }

    /// <summary>
    ///     Hit enemies with specific speed and damage.
    /// </summary>
    /// <param name="speedAttack">
    ///     Speed of attack
    /// </param>
    /// <param name="damage"></param>
    protected void HitEnemy(float speedAttack, float damage)
    {
        if ((Time.time - this._lastHitTime) < this._timeToWait)
        {
            return;
        }
        this._lastHitTime = Time.time;
        this._timeToWait = speedAttack;
        List<Entity> newTargets = new List<Entity>(this._targets);

        for (int i = 0, size = this.shrineManager.shrines.Count; i < size; ++i)
        {
            this.shrineManager.shrines[i].ApplyDamageEffect(ref damage);
            this.shrineManager.shrines[i].ShowVisualEffect(this.transform.position, this.transform.rotation);
        }

        for (int i = 0, size = this._targets.Count; i < size; ++i)
        {
            for (int j = 0, sizeJ = this.shrineManager.shrines.Count; j < sizeJ; ++j)
                this.shrineManager.shrines[i].ApplyEntityEffect(this._targets[i]);
            if (this._targets[i].DealDamage(damage) == true)
                newTargets.Remove(this._targets[i]);
        }
        if (this._targets.Count > 0)
        {
            for (int i = 0, size = this.shrineManager.shrines.Count; i < size; ++i)
                this.shrineManager.shrines[i].UseCharge();
        }
        this._targets = newTargets;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.isTrigger == true ||
            this.transform.IsChildOf(col.transform) == true)
            return ;
        Entity target = col.GetComponentInChildren<Entity>();

        if (target != null)
            this._targets.Add(target);
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.isTrigger == true)
            return;
        Entity target = col.GetComponentInChildren<Entity>();

        if (target != null)
            this._targets.Remove(target);
    }
}
