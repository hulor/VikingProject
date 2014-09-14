using UnityEngine;
using System.Collections;

public class OnDieInstantiate : OnDieEvent
{
    public GameObject objToInstanciate;

    protected override void OnDie()
    {
        GameObject.Instantiate(this.objToInstanciate, this.transform.position, this.transform.rotation);
    }
}
