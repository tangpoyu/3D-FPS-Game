using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Role
{
    PLAYER,
    ENEMY
}

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;

    private float health = 100;

    [SerializeField]
    private Role role;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        switch (role)
        {
            case Role.PLAYER:
                break;

            case Role.ENEMY:
                enemyAnimator = GetComponent<EnemyAnimator>();
                enemyController = GetComponent<EnemyController>();
                navMeshAgent = GetComponent<NavMeshAgent>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        print(gameObject.name + " : " + health + "pp.");

        switch (role)
        {
            case Role.PLAYER:
                // TODO: update of player's health stat ui immediately 
                break;

            case Role.ENEMY: 
                if(enemyController.EnemyState == EnemyState.PATROL)
                {
                 enemyController.ChaseDistance = 30;
                }
                break;
        }

        if(health <= 0)
        {
            PlayerDied();
            isDead = true;
        }
    }

    private void PlayerDied()
    {
        switch(role)
        {
            case Role.PLAYER:
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyController>().enabled = false;
                }

                // TODO : Call enemy manager to stop spawning enemies

                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().SelectedWeapon.gameObject.SetActive(false);
                break;

            case Role.ENEMY:
                string enemyName = transform.name;
                switch (enemyName)
                {
                    case Tags.CANNIBAL:
                        //GetComponent<Animator>().enabled = false;
                        //GetComponent<BoxCollider>().enabled = false;
                        //GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
                        //enemyController.enabled = false;
                        //navMeshAgent.enabled = false;
                        //enemyAnimator.enabled = false;

                        Destroy(gameObject);

                        // TODO : StartCoroutine, and spawn more enemies
                        break;

                    case Tags.BOAR:
                        navMeshAgent.velocity = Vector3.zero;
                        navMeshAgent.isStopped = true;
                        enemyController.enabled = false;
                        enemyAnimator.Dead();
                        // TODO : StartCoroutine, and spawn more enemies
                        break;
                }
                break;
        }

        if(role == Role.PLAYER)
        {
            Invoke("RestartGame", 3f);
        } else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private void TurnOffGameObject()
    {
        gameObject.SetActive(false);

    }
}
