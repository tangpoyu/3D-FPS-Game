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
    private float walkSpeed = 0.5f, runSpeed = 4f;
    private float chaseDistance = 7f, currentChaseDistance, attackDistance = 1.8f, chaseAfterAttackDistance = 2f;
    private float patrolRadiusMin = 20f, patrolRadiusMax = 60f, patrolForThisTime = 15f, patrolTimer;
    private float waitBeforeAttack = 2f, attackTimer;


    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if(enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if(enemyState == EnemyState.ATTACK)
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
        }
        if (Vector3.Distance(transform.position, attackTarget.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(attackTarget.position);
        enemyAnimator.Run(navMeshAgent.velocity.sqrMagnitude > 0 ? true : false);
        if (Vector3.Distance(transform.position, attackTarget.position) <= attackDistance)
        {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        } else if(Vector3.Distance(transform.position, attackTarget.position) > chaseDistance) 
        {
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;
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
            enemyState = EnemyState.CHASE;
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
}
