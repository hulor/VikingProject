using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class Move : FSM.FSMState
    {
        public Move(List<TransitionInfo> trans, Dictionary<eTransition, TransitionInfo.eTransitionType> transType, FSMSystem fsm) :
            base(trans, transType, fsm, eStateId.Move)
        {
        }
    }
}
