using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    public static List<Transform> nodes;
    bool startSpawning;

    float timer;
    public float timeToSpawn;

    [SerializeField] GameObject enemy;

    private void Start()
    {
        nodes = new List<Transform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            startSpawning = true;
        }

        if (startSpawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Instantiate(enemy, transform.position, Quaternion.identity, transform);
                timer = timeToSpawn;
            }
        }

    }
}
