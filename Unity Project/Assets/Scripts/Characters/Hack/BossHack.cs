using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HackSceneReference;

public class BossHack : EnemyHack
{

    public GameObject shield;
    public GameObject shieldExp;

    public float rotationSpeed = 5f;
    
    private Action GetDestination;
    private Vector3 destination;
    private bool hasShield = true;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        //Por algum motivo o boss tava aparecendo rotacionado quando inicia o hack pelo mainScene
        transform.rotation = Quaternion.Euler(0, 0, 0);

        GetDestination = GetRunawayPath;

        if (FindObjectOfType<HackSceneReference>().GetDifficulty() <= EDifficulty.EASY)
            shield.SetActive(false);
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        GetDestination();
        agent.SetDestination(destination);
    }

    void Update()
    {
        if (!hasShield)
            weapon.Fire(mainBarrel);
    }
    
    public void SetAgressive()
    {
        GetDestination = GetAgressivePath;
    }

    private void GetAgressivePath()
    {
        if (GetTarget() != null)
            destination = GetTarget().position;
    }

    private void GetRunawayPath()
    {   
        if (GetTarget() != null)
            if (Vector3.Distance(transform.position, GetTarget().position) < 5f)
                destination = transform.position + ((transform.position - GetTarget().position) * 2); //TESTAR ESSE 2
    }

    public void DisableShield()
    {
        hasShield = false;

        if (FindObjectOfType<HackSceneReference>().GetDifficulty() >= EDifficulty.EASY)
            shieldExp.SetActive(true);

        shield.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Character>().Kill();
        }
    }

}
