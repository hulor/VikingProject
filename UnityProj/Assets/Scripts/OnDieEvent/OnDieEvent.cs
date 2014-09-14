using UnityEngine;
using System.Collections;

public abstract class OnDieEvent : MonoBehaviour
{
    protected virtual void Start()
    {
        Entity entity = this.GetComponent<Entity>();

        if (entity != null)
            entity.dieEvent += this.OnDie;
    }

    protected abstract void OnDie();
}
