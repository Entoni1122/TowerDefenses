using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    public bool startSpawning;
    float timer;
    public float timeToSpawn;

    public static Transform enemyTarget;
    private void Awake()
    {
        enemyTarget = transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            startSpawning = true;
        }
        if (startSpawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Vector3 spawnEnemy = Random.onUnitSphere * 20;
                spawnEnemy.y = 0;
                Vector3 enem  = transform.position + spawnEnemy;
                PoolingMethod.SpawnObject(enemy, enem + Vector3.up * 10, Quaternion.identity);
                timer = timeToSpawn;
            }
        }
    }
}
