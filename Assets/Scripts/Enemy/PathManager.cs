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
    [SerializeField] float radiusSpawner;

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
                Vector3 spawnEnemy = Random.onUnitSphere * radiusSpawner;
                spawnEnemy.y = 0;
                Vector3 enem  = transform.position + spawnEnemy;
                PoolingMethod.SpawnObject(enemy, enem, Quaternion.identity);
                timer = timeToSpawn;
            }
        }
    }
}
