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
        enemy.transform.position = transform.position;
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
                Vector3 spawnEnemy = Random.insideUnitCircle * 20;
                PoolingMethod.SpawnObject(enemy, spawnEnemy, Quaternion.identity);
                timer = timeToSpawn;
            }
        }
    }
}
