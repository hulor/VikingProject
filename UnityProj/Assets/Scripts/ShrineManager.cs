using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///     Manage shrine binded to player.
/// </summary>
public class ShrineManager : MonoBehaviour
{
    public List<ShrineBase> shrines = new List<ShrineBase>();

    private List<ShrineBase> toRemove = new List<ShrineBase>();

    private Entity _entity;


    private void Start()
    {
        WeaponBase[] weapons = this.GetComponentsInChildren<WeaponBase>();

        foreach (WeaponBase weapon in weapons)
            weapon.shrineManager = this;
        this._entity = this.GetComponentInChildren<Entity>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    for (int i = 0, size = this.toRemove.Count; i < size; ++i)
        {
            this.shrines[i].SpentEffect(this._entity.transform.position, this._entity.transform.rotation);
            this.shrines.Remove(this.toRemove[i]);
        }
        this.toRemove.Clear();
	}

    public void PickShrine(ShrineBase shrine)
    {
        this.shrines.Add(shrine);
        shrine.transform.parent = this.transform;
        shrine.OnAttachEntity(this._entity);
        shrine.OnSpentEvent += this.OnShrineElapsed;
    }

    private void OnShrineElapsed(ShrineBase shrine)
    {
        this.toRemove.Add(shrine);
    }
}
