using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    [SerializeField]
    private GameObject boarPrefab, cannibalPrefab;
    [SerializeField]
    private Transform[] cannibalSpawnPoint, boarSpawnPoint;
    [SerializeField]
    private int cannibalCountMax, boarCountMax;
    
    [SerializeField]
    private float waitBeforeSpawnEnemyTime = 10f;

    public int CannibalCountMax { get => cannibalCountMax; set => cannibalCountMax = value; }
    public int BoarCountMax { get => boarCountMax; set => boarCountMax = value; }
    public static EnemyManager Instance { get => instance;}

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

  
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckToSpawnEnemy");
    }

    private int spawn = 1;
    IEnumerator CheckToSpawnEnemy()
    {
        yield return new WaitForSeconds(waitBeforeSpawnEnemyTime);
        //print(spawn++ + " : Checkspawn");
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemy");
    }

    private void SpawnEnemies()
    {
        SpawnCannibal();
        SpawnBoar();
    }

    private void SpawnCannibal()
    {
        int index = 0;
        for(int i = 0; i < cannibalCountMax; i++)
        {
            if(index >= cannibalSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(cannibalPrefab, cannibalSpawnPoint[index].position, Quaternion.identity).name = cannibalPrefab.name;
            index++;
        }

        cannibalCountMax = 0;
    }

    private void SpawnBoar()
    {
        int index = 0;
        for (int i = 0; i < boarCountMax; i++)
        {
            if (index >= boarSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(boarPrefab, boarSpawnPoint[index].position, Quaternion.identity).name = boarPrefab.name;
            index++;
        }

        boarCountMax = 0;
    }
}
