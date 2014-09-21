using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///     TODO
/// </summary>
public class ShrineBase : MonoBehaviour
{
    public delegate void OnSpentHandler(ShrineBase shrine);

    public event OnSpentHandler OnSpentEvent;

    /// <summary>
    ///     Charges numbers of shrine.
    /// </summary>
    public int maxCharges = 30;

    /// <summary>
    ///     Visual effect of shrine, eg. : arcanic explosion, fire tails, etc...
    /// </summary>
    public GameObject visualEffet;

    public GameObject weaponEffect;

    public GameObject dieEffect;

    private int _currentCharges;

    private List<GameObject> _weaponEffectActif = new List<GameObject>();



    private void Start()
    {
        this.ResetShrine();
    }

    public virtual void OnAttachEntity(Entity target)
    {
        WeaponBase[] weapons = target.GetComponentsInChildren<WeaponBase>();
        GameObject effectTmp;

        if (this.weaponEffect == null)
            return;
        foreach (WeaponBase weapon in weapons)
        {
            effectTmp = GameObject.Instantiate(this.weaponEffect) as GameObject;

            if (weapon.mesh != null)
            {
                effectTmp.transform.parent = weapon.mesh.transform;
                effectTmp.transform.localPosition = Vector3.zero;
                effectTmp.transform.localRotation = Quaternion.identity;
                effectTmp.transform.localScale = Vector3.one;
                this._weaponEffectActif.Add(effectTmp);
            }
        }
    }

    public virtual void OnDetachEntity()
    {
        for (int i = 0, size = this._weaponEffectActif.Count; i < size; ++i)
        {
            GameObject.Destroy(this._weaponEffectActif[i]);
        }
    }

    public virtual void ResetShrine()
    {
        this._currentCharges = this.maxCharges;
    }

    public virtual void ApplyDamageEffect(ref float damage)
    {
        //--this._currentCharges;
    }

    public virtual void ApplyEntityEffect(Entity target)
    {
        //--this._currentCharges;
    }

    public virtual void SpentEffect(Vector3 playerPos, Quaternion playerRot)
    {
        if (this.dieEffect != null)
        {
            GameObject go = GameObject.Instantiate(this.dieEffect, playerPos, playerRot) as GameObject;
        }
        for (int i = 0, size = this._weaponEffectActif.Count; i < size; ++i)
        {
            GameObject.Destroy(this._weaponEffectActif[i]);
        }
    }

    public virtual void UseCharge()
    {
        --this._currentCharges;
        if (this._currentCharges == 0 &&
            this.OnSpentEvent != null)
            this.OnSpentEvent(this);
    }

    public virtual void ShowVisualEffect(Vector3 position, Quaternion rotation)
    {
        if (this.visualEffet == null)
            return;
        GameObject effect = GameObject.Instantiate(this.visualEffet, position, rotation) as GameObject;
    }
}
