using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class FSMSystem
    {
        private Dictionary<eStateId, FSMState> _states = new Dictionary<eStateId, FSMState>();
        private List<eStateId> _stateStack = new List<eStateId>();
        private GameObject _npc;

        public FSMSystem(GameObject npcBinded)
        {
            this._npc = npcBinded;
        }

        public void AddState(FSMState state)
        {
            this._states.Add(state.stateId, state);
        }

        public void AddState(List<FSMState> states)
        {
            foreach (FSMState state in states)
            {
                this.AddState(state);
            }
        }

        public void UpdateStats()
        {
            for (int i = this._stateStack.Count; i >= 0; --i)
            {
                this._states[this._stateStack[i]].OnStateUpdate(this._npc);
            }
        }

        public void PerformTransition(TransitionInfo transition)
        {
            for (int i = this._stateStack.Count; i >= 0; --i)
            {
                if (this._states[this._stateStack[i]].ReceiveTransition(transition) == true)
                    break;
            }
        }

        public void SetState(FSMState state)
        {
            if (this._stateStack.Count != 0)
                this.OnStatFinished(this._states[this._stateStack[0]]);
            this._stateStack.Add(state.stateId);
            state.OnStateEnter();
        }

        public void SetState(eStateId stateId)
        {
            if (this._stateStack.Count != 0)
                this.OnStatFinished(this._states[this._stateStack[0]]);
            this._stateStack.Add(stateId);
            this._states[stateId].OnStateEnter();
        }

        public void OnStatFinished(FSMState state)
        {
            int idChild = this.FindStat(state);

            this.ParentKillChildren(idChild);
            state.OnStateLeave();
            this._stateStack.RemoveAt(this._stateStack.Count - 1);
            this._states[this._stateStack[this._stateStack.Count - 1]].OnChildFinished();
        }

        public void StatTransitTo(FSMState state, eStateId destState, TransitionData tData)
        {
            this.OnStatFinished(state);
            if (this._stateStack.Count != 0)
                this.StatAddChild(this._states[this._stateStack[this._stateStack.Count - 1]], destState, tData);
        }

        public void ParentKillChildren(int idChild)
        {
            if (idChild == -1)
            {
                Debug.LogWarning("OnStatFinished : stat not found");
                return;
            }
            if (idChild == this._stateStack.Count)
                return;
            for (int i = this._stateStack.Count; i >= 0; --i)
            {
                this._states[this._stateStack[i]].OnParentAbort();
                if (i == idChild)
                {
                    break;
                }
            }
            this._stateStack.RemoveRange(idChild + 1, this._stateStack.Count - idChild);
        }

        public void StatAddChild(FSMState parent, eStateId childType, TransitionData tData)
        {
            int idParent = this.FindStat(parent);

            if (idParent == -1)
            {
                Debug.LogWarning("StatAddChild : parent not found");
                return;
            }
            if (idParent != this._stateStack.Count)
            {
                this.ParentKillChildren(idParent);
            }
            this._stateStack.Add(childType);
            this._states[childType].OnStateEnter();
        }

        private int FindStat(FSMState stat)
        {
            for (int i = 0, size = this._stateStack.Count; i < size; ++i)
            {
                if (this._states[this._stateStack[i]] == stat)
                {
                    return (i);
                }
            }
            return (-1);
        }

#region DEBUG
        public void ShowStates()
        {
            string states = "";

            for(int i = 0, size = this._stateStack.Count; i < size; ++i)
            {
                states += this._states[this._stateStack[i]].stateId.ToString();
                if (i < size - 1)
                    states += " > ";
            }
            Debug.Log(states);
        }
#endregion
    }
}
