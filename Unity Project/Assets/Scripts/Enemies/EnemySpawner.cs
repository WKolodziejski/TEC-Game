using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public enum Orientation { Horizontal, Vertical, Diagonal };
    public Orientation levelOrientation;


    public float spawnCooldown = 5;
    Transform cameraTransf;
    Camera camera;
    float nextSpawnTime;
    float spawnBoundLeft, spawnBoundRight, spawnBoundUp, spawnBoundDown;

    void Start()
    {
        setCameraPos();
        setCamera();
        updateSpawnTime();
        setBoundries();

    }

    void Update() //adicionar um limite de inimigos e melhor checagem de spawn
    {
        nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime < 0) 
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(enemy, getSpawnCoord(), Quaternion.identity);
        updateSpawnTime();
    }

    private void updateSpawnTime()
    {
        nextSpawnTime = spawnCooldown; //talvez adicionar aleatoriedade
    }

    private Vector2 getSpawnCoord()
    {
        return new Vector2(getSpawnX(), getSpawnY());
    }

    private float getSpawnX()
    {
        return Random.Range(cameraTransf.position.x + spawnBoundLeft, cameraTransf.position.y + spawnBoundRight);
    }

    private float getSpawnY()
    {
        return Random.Range(cameraTransf.position.y + spawnBoundDown, cameraTransf.position.y + spawnBoundUp);
    }

    private void setBoundries() //armazenar apenas os multiplicadores, caso a mudança de resolução se torne um problema
    {
        float horizontalSize = getHorizontalSize();
        float verticalSize = getVerticalSize();

        if (levelOrientation == Orientation.Horizontal)
        {
            spawnBoundLeft = horizontalSize * 0.5f;
            spawnBoundRight = horizontalSize * 1.5f;
            spawnBoundDown = verticalSize * -0.5f;
            spawnBoundUp = verticalSize * 0.5f;
        } else
        {
            if (levelOrientation == Orientation.Vertical)
            {
                spawnBoundLeft = horizontalSize * -0.5f;
                spawnBoundRight = horizontalSize * 0.5f;
                spawnBoundDown = verticalSize * 0.5f;
                spawnBoundUp = verticalSize * 1.5f;
            } else
            {
                spawnBoundLeft = horizontalSize * 0.5f;
                spawnBoundRight = horizontalSize * 1.5f;
                spawnBoundDown = verticalSize * -0.5f;
                spawnBoundUp = verticalSize * 1.5f;
            }
        }
    }

    private float getVerticalSize()
    {
        return camera.orthographicSize * 2;
    }

    private float getHorizontalSize()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height; //problema com mudança de resolução 
        return getVerticalSize() * screenAspect; 
    }

    private void setCameraPos()
    {
        cameraTransf = Camera.main.transform;
    }

    private void setCamera()
    {
        camera = Camera.main;
    }

}
