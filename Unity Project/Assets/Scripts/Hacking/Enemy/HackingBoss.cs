using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HackSceneReference;

public class HackingBoss : HackingEnemy
{
    public float coolDown = 0f; //redundante com o do player, mas fazer o que?
    public GameObject shield;
    private float lastCooldown;

    public float rotationSpeed = 5f;
    public Transform barrel1;
    public Transform barrel2;
    public Transform barrelS;
    public GameObject shieldExp;

    private Action GetDestination;
    private Vector3 destination;

    void Start()
    {
        setTarget();
        setWeapon();
        setNavMash();

        GetDestination += GetRunawayPath;

        if (HackSceneReference.Instance.GetDifficulty() == EDifficulty.EASY)
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
        weapon.Fire(barrel1);
        //...

        
    }
    public override void TakeDamage(float damage)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + coolDown;

            hp -= damage;

            if (popup != null)
                Instantiate(popup, transform.position, Quaternion.identity, transform).Hit(damage);

            if (hp <= 0)
            {
                onDie?.Invoke();
                OnDie();
            }
            else
            {
                if (shield != null)
                    Destroy(Instantiate(shield, transform), 1f);
            }
        }
    }

    public void SetAgressive()
    {
        GetDestination += GetAgressivePath;
    }

    private void GetAgressivePath()
    {
        destination = target.position;
    }

    private void GetRunawayPath()
    {        
        if (Vector3.Distance(transform.position, target.position) < 5f)
            destination = transform.position + ((transform.position - target.position) * 2); //TESTAR ESSE 2
    }

    public void DisableShield()
    {
        if (HackSceneReference.Instance.GetDifficulty() != EDifficulty.EASY)
            shieldExp.SetActive(true);

        shield.SetActive(false);
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }

}
