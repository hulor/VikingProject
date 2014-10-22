using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum eStateId
    {
        NoStatId = 0, // should never be assigned
        Idle = 1,
        Move = 2,
        Attack = 3,
    }
}
