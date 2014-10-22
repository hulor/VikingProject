using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class Idle : FSM.FSMState
    {
        public Idle(List<TransitionInfo> transitions, Dictionary<eTransition, TransitionInfo.eTransitionType> transitionType, FSMSystem fsm)
            : base(transitions, transitionType, fsm, eStateId.Idle)
        {
        }
    }
}
