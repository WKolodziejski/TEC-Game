using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class EnemySpawnerPoint : MonoBehaviour
{
    public GameObject enemy;
    public float spawnCooldown = 2;
    public int enemyLimit = 5;
    Transform cameraT;
    float nextSpawnTime;
    private ArrayList enemyList;

    void Start()
    {
        enemyList = new ArrayList(enemyLimit);
        cameraT = Camera.main.transform;
        updateSpawnTime();
        //setSpawnBoundriesMult();
    }

    void FixedUpdate()
    {
        nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime < 0f) 
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if ( enemyList.Count < enemyLimit){
            GameObject e = Instantiate(enemy, transform.position, quaternion.identity);
            e.GetComponent<Enemy2D>().SetOnDieListener(() =>
                {
                    enemyList.Remove(e);
                });
            enemyList.Add(e);
            updateSpawnTime();
        }
    }

    private void updateSpawnTime()
    {
        nextSpawnTime = spawnCooldown;
    }

    public float[] getCameraBoundries() 
    {
        /* CAMERA*/
        return null;
    }
}