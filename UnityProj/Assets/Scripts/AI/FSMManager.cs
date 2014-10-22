using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMManager : MonoBehaviour
{
    private FSM.FSMSystem fsm;

	// Use this for initialization
	void Start ()
    {
        this.fsm = new FSM.FSMSystem(this.gameObject);
        List<FSM.FSMState> states = new List<FSM.FSMState>();
        List<FSM.TransitionInfo> trans = new List<FSM.TransitionInfo>();
        Dictionary<FSM.eTransition, FSM.TransitionInfo.eTransitionType> transType = new Dictionary<FSM.eTransition,FSM.TransitionInfo.eTransitionType>();

        trans.Add(new FSM.TransitionInfo(FSM.TransitionInfo.eTransitionType.NoneTransition, FSM.eStateId.Attack, FSM.eTransition.EnemyInTrigger, null));
        transType.Add(FSM.eTransition.EnemyInTrigger, FSM.TransitionInfo.eTransitionType.AddChild);
        states.Add(new FSM.Idle(trans, transType, this.fsm));
        trans.Clear();
        transType.Clear();
        states.Add(new FSM.Attack(trans, transType, this.fsm));
        this.fsm.AddState(states);
        this.fsm.SetState(FSM.eStateId.Idle);
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.fsm.UpdateStats();
        this.fsm.ShowStates();
	}
}
