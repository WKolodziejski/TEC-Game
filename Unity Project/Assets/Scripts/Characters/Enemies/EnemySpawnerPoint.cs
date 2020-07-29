using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnerPoint : MonoBehaviour
{
    public GameObject enemy;
    public float spawnCooldown = 2;
    public int enemyLimit = 5;
    float nextSpawnTime;
    private ArrayList enemyList;

    void Start()
    {
        enemyList = new ArrayList(enemyLimit);
        nextSpawnTime = spawnCooldown;
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
        if ((enemyList.Count < enemyLimit) && !GetComponent<Renderer>().IsVisibleFrom(Camera.main)){
            GameObject e = Instantiate(enemy, transform.position, quaternion.identity);
            e.GetComponent<Enemy2D>().SetOnDieListener(() =>
                {
                    enemyList.Remove(e);
                });
            enemyList.Add(e);
            nextSpawnTime = spawnCooldown;
        }
    }

    public void SetEnabled(bool enabled){
        this.enabled = enabled;
    }
}