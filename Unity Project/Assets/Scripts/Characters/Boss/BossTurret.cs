using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour
{
    private const int MAX_TURN = 45;
    public Bullet bullet;
    private Character target;
    private float startAngle;
    private float angle;
    public float fireRate = 0.5f;
    private float lastCooldown = 2;

    private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.eulerAngles.z;

        GetTarget();

        firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) GetTarget();
        angle = GetAngle();
        SetAngle(angle);
        Shoot();
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    } 

    private float GetAngle()
    {
        float ang;
        
        ang = Vector2.SignedAngle(target.transform.position, transform.position);

        if (ang > - MAX_TURN && ang < MAX_TURN) 
            return ang;

        return MAX_TURN;
    }

    private void SetAngle(float ang)
    {
        //float ang;

        //ang = Vector2.SignedAngle(target.transform.position, transform.position);

        /*if (ang > - MAX_TURN && ang < MAX_TURN)
        {
            transform.rotation = Quaternion.Euler(0, 0, startAngle - ang);
        }*/

        if (ang != MAX_TURN)
            transform.rotation = Quaternion.Euler(0, 0, startAngle - ang);
    }

    public void Shoot() {
        if (angle == MAX_TURN) return;

        if (lastCooldown > Time.time) return;
        
        lastCooldown = Time.time + (1f / fireRate);

        Bullet b = Instantiate(bullet, firePoint.position, firePoint.rotation);
        b.Fire(0);
    }
}
