using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    public static List<Transform> nodes;
    public static List<GameObject> HexagonEnemyDIOPO;
    bool startSpawning;

    float timer;
    public float timeToSpawn;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject EnemySpawn;
    [SerializeField] GameObject Nexus;
    [SerializeField] GameObject EnemyHexagon;


    private void Start()
    {
        nodes = new List<Transform>();
        HexagonEnemyDIOPO = new List<GameObject>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(EnemySpawn, nodes[0].transform.position,Quaternion.identity);
            Instantiate(Nexus, nodes[nodes.Count - 1].transform.position, Quaternion.identity);
            startSpawning = true;
        }

        if (startSpawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                PoolingMethod.SpawnObject(enemy, HexagonEnemyDIOPO[0].transform.position, Quaternion.identity);
                timer = timeToSpawn;
            }
        }
    }
}
