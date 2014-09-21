using UnityEngine;
using System.Collections;

public class ArcanicShrine : ShrineBase
{
    public float damageAdded = -5.0f;

    public override void ApplyDamageEffect(ref float damage)
    {
        damage += this.damageAdded;
        base.ApplyDamageEffect(ref damage);
    }

    public override void ApplyEntityEffect(Entity target)
    {
    }
}
