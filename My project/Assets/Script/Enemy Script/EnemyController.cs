using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyState enemyState;
    private Transform attackTarget;
    [SerializeField]
    private float walkSpeed = 0.5f, runSpeed = 4f;
    [SerializeField]
    private float chaseDistance = 7f, attackDistance = 1.8f, chaseAfterAttackDistance = 2f;
    private float currentChaseDistance;
    [SerializeField]
    private float patrolRadiusMin = 20f, patrolRadiusMax = 60f, patrolForThisTime = 15f;
    private float patrolTimer;
    [SerializeField]
    private float waitBeforeAttack = 2f;
    private float attackTimer;

    [SerializeField]
    private GameObject attackPoint;

    private EnemyAudio enemyAudio;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }
    public float ChaseDistance { get => chaseDistance; set => chaseDistance = value; }
    public float AttackDistance { get => attackDistance; set => attackDistance = value; }
    public float ChaseAfterAttackDistance { get => chaseAfterAttackDistance; set => chaseAfterAttackDistance = value; }

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if(EnemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if(EnemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    private void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        attackTimer += Time.deltaTime;
        if(attackTimer > waitBeforeAttack)
        {
            enemyAnimator.Attack();
            attackTimer = 0;
            enemyAudio.PlayAttackSound();
        }
        if (Vector3.Distance(transform.position, attackTarget.position) > attackDistance + chaseAfterAttackDistance)
        {
            EnemyState = EnemyState.CHASE;
        }
    }

    private void Chase()
    {
      //  print(Vector3.Distance(transform.position, attackTarget.position));
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(attackTarget.position);
        enemyAnimator.Run(navMeshAgent.velocity.sqrMagnitude > 0 ? true : false);
        if (Vector3.Distance(transform.position, attackTarget.position) <= attackDistance)
        {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            EnemyState = EnemyState.ATTACK;

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        } else if(Vector3.Distance(transform.position, attackTarget.position) > chaseDistance) 
        {
            enemyAnimator.Run(false);
            EnemyState = EnemyState.PATROL;
            patrolTimer = patrolForThisTime;
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    private void Patrol()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        enemyAnimator.Walk(navMeshAgent.velocity.sqrMagnitude > 0 ? true : false);
        
        if (Vector3.Distance(transform.position, attackTarget.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            EnemyState = EnemyState.CHASE;
            enemyAudio.PlayScreamSound();
        }
    }

    private void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navMeshAgent.SetDestination(navHit.position);
    }

    public void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    public void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy) attackPoint.SetActive(false);
    }
}
