using UnityEngine;
using System.Collections;

/// <summary>
///     Shrine sealed can be picked up.
/// </summary>
public class SealedShrine : MonoBehaviour
{
    public ShrineBase shrine;
    public GameObject sealComplex;
    public GameObject sealUsedEffect;

    public void OnTriggerEnter(Collider col)
    {
        ShrineManager shrineManager = col.GetComponentInChildren<ShrineManager>();

        if (shrineManager != null)
        {
            shrineManager.PickShrine(this.shrine);
            GameObject.Instantiate(this.sealUsedEffect, this.transform.position, this.transform.rotation);
            GameObject.Destroy(this.sealComplex);
        }
    }
}
