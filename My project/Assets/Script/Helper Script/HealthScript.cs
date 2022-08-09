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

    private EnemyAudio enemyAudio;
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
                enemyAudio = GetComponentInChildren<EnemyAudio>();
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
        if(role == Role.ENEMY)
        {
            print(gameObject.name + " : " + health + "pp.");
        }

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
                        float degree = 0;
                        enemyController.enabled = false;
                        GetComponent<Animator>().enabled = false;
                        navMeshAgent.enabled = false;
                        enemyAnimator.enabled = false;
                        StartCoroutine(DeadSound());
                        StartCoroutine(Dead(degree));

                        // TODO : spawn more enemies
                        break;

                    case Tags.BOAR:
                        navMeshAgent.velocity = Vector3.zero;
                        navMeshAgent.isStopped = true;
                        enemyController.enabled = false;
                        StartCoroutine(DeadSound());
                        enemyAnimator.Dead();
                        Invoke("TurnOffGameObject", 3f);
                        // TODO : spawn more enemies
                        break;
                }
                break;
        }

        if(role == Role.PLAYER)
        {
            Invoke("RestartGame", 3f);
        } 
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private void TurnOffGameObject()
    {
        Destroy(gameObject);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDieSound();
    }

    IEnumerator Dead(float degree)
    {
        yield return new WaitForSeconds(0.1f);
        if(degree <= -90)
            Invoke("TurnOffGameObject",0f);
        else
        {
            transform.rotation = Quaternion.EulerAngles(degree--, 0, 0);
            StartCoroutine(Dead(degree));
        }
    
    }
}
