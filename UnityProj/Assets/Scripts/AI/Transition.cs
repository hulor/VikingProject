using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum eTransition
    {
        NoTransition = 0, //should never be fired
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
            SimpleTransition = 1
        };

        /// <summary>
        ///     Type of transition.
        /// </summary>
        public eTransitionType transitionType;

        /// <summary>
        ///     Destination stat.
        /// </summary>
        public eStatId idDest;

        /// <summary>
        ///     transition type.
        /// </summary>
        public eTransition transition;

        public TransitionInfo(eTransitionType type, eStatId dest, eTransition transition)
        {
            this.transitionType = type;
            this.idDest = dest;
            this.transition = transition;
        }
    }

    public struct TransitionData
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
