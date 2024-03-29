using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawne : MonoBehaviour
{
    float timer;
    public float timeToSpawn ;

    [SerializeField] GameObject enemy;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Instantiate(enemy,transform.position,Quaternion.identity,transform);
            timer = timeToSpawn;
        }
    }
}
