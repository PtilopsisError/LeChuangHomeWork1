using UnityEngine;

public class EnemiesAttacking : IState
{
    private EnemiesAI fsm;
    private int attackCdCounter = 0;
    private int stiffCounter = 0;

    public EnemiesAttacking(FSM fsm){
        this.fsm = (EnemiesAI)fsm;
    }

    void IState.OnEnter(){
        fsm.navMeshAgent.isStopped = true;
        fsm.GetComponent<Rigidbody>().velocity = Vector3.zero;
        attackCdCounter = fsm.attackCd;
    }

    void IState.OnExit(){
        fsm.navMeshAgent.isStopped = false;
    }

    void IState.OnFixedUpdate(){
        if(attackCdCounter > 0){
            attackCdCounter --;
            if(stiffCounter > 0){
                stiffCounter --;
            }else{
                float distanceToPlayer = Vector3.Distance(fsm.transform.position, fsm.playerTransform.position);
                if(distanceToPlayer < fsm.attackRadius){
                    fsm.GetComponent<Rigidbody>().velocity = fsm.GetComponent<Rigidbody>().transform.forward * -0.25f *fsm.moveSpeed; 
                }
            }
        }else{
            if(fsm.Attack()){
                attackCdCounter = fsm.attackCd;
                stiffCounter = 25;
            }
        }
    }

    void IState.OnUpdate(){
        fsm.transform.LookAt(fsm.playerTransform);
        if(stiffCounter == 0){
            float distanceToPlayer = Vector3.Distance(fsm.transform.position, fsm.playerTransform.position);
            if(distanceToPlayer > fsm.attackRadius){
                fsm.SwitchState(StateType.Tracing);
            }
        }
    }
}
