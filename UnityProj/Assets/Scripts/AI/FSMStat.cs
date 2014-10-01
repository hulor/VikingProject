using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public enum eStatId
    {
        NoStatId = 0, // should never be assigned
    }

    public abstract class FSMStat
    {
        /// <summary>
        ///     Which state is it?
        /// </summary>
        public readonly FSM.eStatId statId;
        protected List<TransitionInfo> _transition;
        protected FSMSystem _fsm;

        protected FSMStat(FSM.eStatId id, List<TransitionInfo> transitions, FSMSystem fsm)
        {
            this.statId = id;
            this._transition = transitions;
            this._fsm = fsm;
        }

        /// <summary>
        ///     Call when the stat enter in StateMachine call list.
        /// </summary>
        public abstract void OnStatEnter();

        /// <summary>
        ///     Update from StateMachine.
        /// </summary>
        /// <param name="npc"></param>
        public abstract void OnStatUpdate(GameObject npc);

        /// <summary>
        ///     Call when the stat is removed from the StateMachine
        /// </summary>
        public abstract void OnStatLeave();

        /// <summary>
        ///     Call when a child stat just finished.
        /// </summary>
        public abstract void OnChildFinished();

        /// <summary>
        ///     Call when a parent stat kill its children.
        /// </summary>
        public abstract void OnParentAbort();

        /// <summary>
        ///     Check if the stat must catch the transition.
        /// </summary>
        /// <param name="transition">
        ///     Transition received.
        /// </param>
        /// <returns>
        ///     Does the stat had catch the transition?
        /// </returns>
        public bool ReceiveTransition(TransitionData transition)
        {
            for (int i = 0, size = this._transition.Count; i < size; ++i)
            {
                if (this._transition[i].transition == transition.transition)
                {
                    // perform transition.
                    return (true);
                }
            }
                //if (this._transition.ContainsKey(transition.transition) == true)
                //{
                //    return (true);
                //}
            return (false);
        }
    }
}
