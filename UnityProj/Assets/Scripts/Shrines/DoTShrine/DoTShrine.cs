using UnityEngine;
using System.Collections;

public class DoTShrine : ShrineBase
{
    public ElementalDoT dotPrefab;

    public override void ApplyDamageEffect(ref float damage)
    {
    }

    public override void ApplyEntityEffect(Entity target)
    {
        ElementalDoT[] dotElem = target.GetComponentsInChildren<ElementalDoT>();

        if (dotElem != null &&
            dotElem.Length != 0)
        {
            for (int i = 0, size = dotElem.Length; i < size; ++i)
            {
                if (dotElem[i].element == this.dotPrefab.element)
                {
                    dotElem[i].ResetTimer();
                    return;
                }
            }
        }
        GameObject dot = GameObject.Instantiate(this.dotPrefab.gameObject) as GameObject;

        dot.transform.parent = target.transform;
        dot.transform.localPosition = Vector3.zero;
        dot.transform.localRotation = Quaternion.identity;
        dot.transform.localScale = Vector3.one;
        dot.GetComponent<ElementalDoT>().entity = target;
    }
}
