using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemiesAI : FSM, IResumeable
{
    private new Rigidbody rigidbody;
    private bool beFrezzed = false;

    public MyGameManager.EnemiesType thisType;
    public float detectRadius = 10f;
    public float attackRadius = 7.5f;
    public float moveSpeed = 12f;
    public float turnSpeed = 180f;
    public int attackCd = 100;
    [Range(0.1f,100f)]
    public float attackStrenght = 25f;

    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform[] spawnPoints;

    public void Awake(){
        states = new Dictionary<StateType, IState>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<MyGameManager>().spawnPoints;
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        states.Add(StateType.Roaming, new EnemiesRoaming(this));
        states.Add(StateType.Attacking, new EnemiesAttacking(this));
        states.Add(StateType.Tracing, new EnemiesTracing(this));
        SwitchState(StateType.Roaming);
    }

    public void LateUpdate(){
        if(beFrezzed){
            Detention();
        }
    }

    public void initializeNav(){
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.angularSpeed = turnSpeed;
        navMeshAgent.isStopped = false;
        rigidbody.freezeRotation = false;
    }

    private void Detention(){
        navMeshAgent.speed = 0;
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.isStopped = true;
        rigidbody.freezeRotation = true;
        rigidbody.velocity = Vector3.zero;
    }

    public bool Attack(){
        if(beFrezzed){
            return false;
        }
        //Debug.Log("attack");
        if(thisType == MyGameManager.EnemiesType.Stricker){
            // rigidbody.velocity = Vector3.zero;
            // rigidbody.AddForce(rigidbody.transform.forward * attackStrenght);
            rigidbody.velocity = rigidbody.transform.forward * attackStrenght;
        }else if(thisType == MyGameManager.EnemiesType.Gunner){
            TankShooting ts = GetComponent<TankShooting>();
            ts.StartCharging();
            ts.EndCharging();
        }
        return true;
    }

    public void Active(){
        //Debug.Log("be frezzed");
        beFrezzed = true;
        //Detention();
    }
    public void Resume(){
        //Debug.Log("frezzed reseum");
        beFrezzed = false;
        initializeNav();
    }
}
