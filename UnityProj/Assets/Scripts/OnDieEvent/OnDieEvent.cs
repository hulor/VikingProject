using UnityEngine;
using System.Collections;

/// <summary>
///     Die event base script. Override OnDie to do some stuff when binded entity dies.
/// </summary>
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
