using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType{
    Roaming,
    Tracing,
    Attacking,
}

public abstract class FSM : MonoBehaviour {
    public IState currentState;
    public Dictionary<StateType, IState> states;

    public void Update(){
        currentState.OnUpdate();
    }

    public void FixedUpdate(){
        currentState.OnFixedUpdate();
    }

    public void SwitchState(StateType stateType){
        if(states[stateType] == null){
            Debug.Log(String.Format("State \"{0}\" dose not exist",stateType));
            return;
        }
        if(currentState == states[stateType]){
            Debug.Log(String.Format("Can not switch state \"{0}\" to itself",stateType));
            return;
        }
        if(currentState != null){
            currentState.OnExit();
        }
        currentState = states[stateType];
        currentState.OnEnter();
        //Debug.Log(String.Format("Swtich to state:{0}", stateType));
    }
}
