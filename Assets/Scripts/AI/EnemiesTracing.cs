using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemiesTracing : IState
{
    private EnemiesAI fsm;

    public EnemiesTracing(FSM fsm){
        this.fsm = (EnemiesAI)fsm;
    }

    void IState.OnEnter(){
        fsm.initializeNav();
        TracePlayer();
    }

    void IState.OnExit(){
        
    }

    void IState.OnFixedUpdate(){
        TracePlayer();
        DetectPlayer();
    }

    void IState.OnUpdate(){
        
    }

    private void TracePlayer(){
        fsm.navMeshAgent.SetDestination(fsm.playerTransform.position);
    }

    private void DetectPlayer(){
        float distanceToPlayer = Vector3.Distance(fsm.transform.position, fsm.playerTransform.position);
        if(distanceToPlayer <= fsm.attackRadius){
            fsm.SwitchState(StateType.Attacking);
        }
    }
}
