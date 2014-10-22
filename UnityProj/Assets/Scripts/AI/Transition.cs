using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum eTransition
    {
        NoTransition = 0, //should never be fired
        EnemyInTrigger = 1,
    }

    public struct TransitionInfo
    {
        public enum eTransitionType
        {
            /// <summary>
            ///     Stat will add a child of type idDest when receive the binded transition.
            /// </summary>
            AddChild = 0,
            /// <summary>
            ///     Stat will kill himself and transition to idDest.
            /// </summary>
            SimpleTransition = 1,
            /// <summary>
            ///     Should never be set.
            /// </summary>
            NoneTransition
        };

        /// <summary>
        ///     Type of transition.
        /// </summary>
        public eTransitionType transitionType;

        /// <summary>
        ///     Destination stat.
        /// </summary>
        public eStateId idDest;

        /// <summary>
        ///     transition type.
        /// </summary>
        public eTransition transition;

        public TransitionData data;

        private delegate void TransitionHandler(FSMSystem sys, FSMState state);

        private TransitionHandler[] _transitions;

        public TransitionInfo(eTransitionType type, eStateId dest, eTransition transition, TransitionData tData)
        {
            this.transitionType = type;
            this.idDest = dest;
            this.transition = transition;
            this.data = tData;
            this._transitions = new TransitionHandler[(int)eTransitionType.NoneTransition];
            this._transitions[(int)eTransitionType.AddChild] = this.AddTransition;
            this._transitions[(int)eTransitionType.SimpleTransition] = this.SwitchStateTransition;
        }

        public void PerformTransition(FSMSystem fsm, FSMState state)
        {
            if (this.transitionType == eTransitionType.NoneTransition)
                return;
            this._transitions[(int)this.transitionType](fsm, state);
        }

        private void AddTransition(FSMSystem fsm, FSMState state)
        {
            fsm.StatAddChild(state, this.idDest, this.data);
        }

        private void SwitchStateTransition(FSMSystem fsm, FSMState state)
        {
            fsm.StatTransitTo(state, this.idDest, this.data);
        }
    }

    public class TransitionData
    {
        public eTransition transition;
        public Object data;

        public TransitionData(eTransition trans, Object data)
        {
            this.transition = trans;
            this.data = data;
        }
    }
}
