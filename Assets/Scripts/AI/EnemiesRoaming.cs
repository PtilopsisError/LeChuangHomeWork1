using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemiesRoaming : IState 
{
    private EnemiesAI fsm;
    private int counter = 0;
    private int destinationFlag = 0;

    public EnemiesRoaming(FSM fsm){
        this.fsm = (EnemiesAI)fsm;
    }

    void IState.OnEnter(){
        fsm.initializeNav();
    }

    void IState.OnExit(){
        
    }

    void IState.OnFixedUpdate(){
        DetectPlayer();
        Roaming();
    }

    void IState.OnUpdate(){
        
    }

    private void DetectPlayer(){
        float distanceToPlayer = Vector3.Distance(fsm.transform.position, fsm.playerTransform.position);
        if(distanceToPlayer <= fsm.detectRadius){
            fsm.SwitchState(StateType.Tracing);
        }
    }

    private void Roaming(){
        if(counter ++ == 150){
            destinationFlag ++;
            destinationFlag %= fsm.spawnPoints.Length;
            fsm.navMeshAgent.SetDestination(fsm.spawnPoints[destinationFlag].position);
            counter = 0;
        }
        if(Vector3.Distance(fsm.transform.position, fsm.spawnPoints[destinationFlag].position) <= 3){
            destinationFlag ++;
            destinationFlag %= fsm.spawnPoints.Length;
            fsm.navMeshAgent.SetDestination(fsm.spawnPoints[destinationFlag].position);
            counter = 0;
        }
    }
}
