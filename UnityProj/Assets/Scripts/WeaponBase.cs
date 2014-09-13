using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    ///     How much weapon hurts.
    /// </summary>
    public float hit = 10.0f;

    /// <summary>
    ///     Weapon attack speed.
    /// </summary>
    public float speedAttack = 1.0f;

    /// <summary>
    ///     Time when last attack was thrown.
    /// </summary>
    private float _lastHitTime = 0.0f;

    /// <summary>
    ///     All shrine applied to weapon.
    /// </summary>
    private List<ShrineBase> _shrines = new List<ShrineBase>();


    public void Attack(Entity target)
    {
        if ((Time.time - this._lastHitTime) < this.speedAttack)
        {
            return;
        }
        target.DealDamage(this.hit * -1.0f);
    }
}
