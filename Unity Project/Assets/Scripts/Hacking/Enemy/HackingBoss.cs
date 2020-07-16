using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HackSceneReference;

public class HackingBoss : HackingCharacter
{

    public float rotationSpeed = 5f;
    public Transform barrel1;
    public Transform barrel2;
    public Transform barrelS;
    public GameObject shield;
    public GameObject shieldExp;

    private NavMeshAgent agent;
    private Transform player;
    private Weapon weapon;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        weapon = GetComponent<Weapon>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        agent.destination = player.position;
    }

    void Update()
    {
        weapon.Fire(barrel1);
        //...

        
    }

    public void SetAgressive()
    {
        /*Vector2 lookDir = player.position - transform.position;

        float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = transform.right * movementSpeed;*/
    }

    public void SetRunaway()
    {
        /*Vector2 lookDir = player.position - transform.position;

        float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = -transform.right * movementSpeed;*/

        
    }

    public void DisableShield()
    {
        if (HackSceneReference.Instance.GetDifficulty() != EDifficulty.EASY)
            shieldExp.SetActive(true);

        shield.SetActive(false);
    }

}
