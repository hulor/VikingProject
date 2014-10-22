using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public abstract class FSMState
    {
        /// <summary>
        ///     Which state is it?
        /// </summary>
        public readonly FSM.eStateId stateId;
        protected List<TransitionInfo> _transition;
        protected Dictionary<eTransition, TransitionInfo.eTransitionType> _transType;
        protected FSMSystem _fsm;
        protected TransitionData tData;

        protected FSMState(List<TransitionInfo> transitions, Dictionary<eTransition, TransitionInfo.eTransitionType> transitionType, FSMSystem fsm, eStateId stat)
        {
            this.stateId = stat;
            this._transition = transitions;
            this._transType = transitionType;
            this._fsm = fsm;
        }

        /// <summary>
        ///     Call when the stat enter in StateMachine call list.
        /// </summary>
        public virtual void OnStateEnter() { }

        /// <summary>
        ///     Update from StateMachine.
        /// </summary>
        /// <param name="npc"></param>
        public virtual void OnStateUpdate(GameObject npc) { }

        /// <summary>
        ///     Call when the stat is removed from the StateMachine
        /// </summary>
        public virtual void OnStateLeave() { }

        /// <summary>
        ///     Call when a child stat just finished.
        /// </summary>
        public virtual void OnChildFinished() { }

        /// <summary>
        ///     Call when a parent stat kill its children.
        /// </summary>
        public virtual void OnParentAbort() { }

        /// <summary>
        ///     Call when a transition Data is set.
        /// </summary>
        protected virtual void OnDataSet() { }

        public void SetData(TransitionData tData)
        {
            this.tData = tData;
            this.OnDataSet();
        }

        /// <summary>
        ///     Check if the stat must catch the transition.
        /// </summary>
        /// <param name="transition">
        ///     Transition received.
        /// </param>
        /// <returns>
        ///     Does the stat had catch the transition?
        /// </returns>
        public bool ReceiveTransition(TransitionInfo transition)
        {
            for (int i = 0, size = this._transition.Count; i < size; ++i)
            {
                if (this._transition[i].transition == transition.transition)
                {
                    transition.transitionType = this._transType[transition.transition];
                    transition.PerformTransition(this._fsm, this);
                    return (true);
                }
            }
            return (false);
        }
    }
}
