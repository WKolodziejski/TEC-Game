using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float minDist = 5;
    public float minDistMult = 2;
    Transform cameraTransf;
    Camera camera;
    float nextSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        iniCameraPos();
        iniCamera();
        iniSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (getHorRightBound() > nextSpawnPoint) //Considerar mudança vertical em proximas versões
        {
            Vector2 where2Spawn = new Vector2(getHorRightBound() + minDist, getSpawnY());
            Instantiate(enemy, where2Spawn, Quaternion.identity);
            updateSpawnPoint();
        }
    }

    private float getSpawnY()
    {
        return UnityEngine.Random.Range(cameraTransf.position.y - camera.orthographicSize, cameraTransf.position.y + camera.orthographicSize);
    }

    private float getHorRightBound()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height; //otimizar? problema com mudança de resolução 
        float halfCameraHeight = camera.orthographicSize;
        return cameraTransf.position.x + (halfCameraHeight * screenAspect); 
    }

    private void updateSpawnPoint()
    {
        nextSpawnPoint += UnityEngine.Random.Range(minDist, minDist*minDistMult);
    }

    private void iniCameraPos()
    {
        cameraTransf = Camera.main.transform;
    }

    private void iniCamera()
    {
        camera = Camera.main;
    }

    private void iniSpawnPoint()
    {
        nextSpawnPoint = getHorRightBound();
    }
}
