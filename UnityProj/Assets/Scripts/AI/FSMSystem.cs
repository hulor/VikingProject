using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class FSMSystem
    {
        private Dictionary<eStatId, FSMStat> _stats = new Dictionary<eStatId, FSMStat>();
        private List<eStatId> _statStack = new List<eStatId>();
        private GameObject _npc;

        public FSMSystem(GameObject npcBinded)
        {
            this._npc = npcBinded;
        }

        public void AddStat(FSMStat stat)
        {
            this._stats.Add(stat.statId, stat);
        }

        public void UpdateStats()
        {
            for (int i = this._statStack.Count; i >= 0; --i)
            {
                this._stats[this._statStack[i]].OnStatUpdate(this._npc);
            }
        }

        public void PerformTransition(TransitionData transition)
        {
            for (int i = 0, size = this._statStack.Count; i < size; ++i)
            {
                this._stats[this._statStack[i]].ReceiveTransition(transition);
            }
        }

        public void OnStatFinished(FSMStat stat)
        {
            int idChild = this.FindStat(stat);

            this.ParentKillChildren(idChild);
            stat.OnStatLeave();
            this._statStack.RemoveAt(this._statStack.Count - 1);
            this._stats[this._statStack[this._statStack.Count - 1]].OnChildFinished();
        }

        public void ParentKillChildren(int idChild)
        {
            if (idChild == -1)
            {
                Debug.LogWarning("OnStatFinished : stat not found");
                return;
            }
            if (idChild == this._statStack.Count)
                return;
            for (int i = this._statStack.Count; i >= 0; --i)
            {
                this._stats[this._statStack[i]].OnParentAbort();
                if (i == idChild)
                {
                    break;
                }
            }
            this._statStack.RemoveRange(idChild + 1, this._statStack.Count - idChild);
        }

        public void StatAddChild(FSMStat parent, eStatId childType)
        {
            int idParent = this.FindStat(parent);

            if (idParent == -1)
            {
                Debug.LogWarning("StatAddChild : parent not found");
                return;
            }
            if (idParent != this._statStack.Count)
            {
                this.ParentKillChildren(parent);
            }
            this._statStack.Add(childType);
            this._stats[childType].OnStatEnter();
        }

        private int FindStat(FSMStat stat)
        {
            for (int i = 0, size = this._statStack.Count; i < size; ++i)
            {
                if (this._stats[this._statStack[i]] == stat)
                {
                    return (i);
                }
            }
            return (-1);
        }
    }
}
