using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : Enemy2D
{
    private const int MAX_TURN = 60;
    public Bullet bullet;
    //private Character target;
    private float startAngle;
    private float angle;
    private float fireRate = 0.5f;
    private float lastCd = 2;
    private bool shoot;
    private BossFirstLayer bossFirstLayer;

    //public int hp = 3;

    private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        bossFirstLayer = this.transform.parent.GetComponentInParent<BossFirstLayer>();
        bossFirstLayer.turrets.Add(this);
        bossFirstLayer.numTurrets += 1;
        fireRate = bossFirstLayer.turretsFireRate;

        startAngle = transform.eulerAngles.z;

        GetTarget();

        firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        shoot = true;
        
        if (target == null) GetTarget();

        angle = GetAngle();
        SetAngle(angle);
        
        Shoot();
    }

    public override void Attack() //TODO: isso aqui nem tá sendo usado
    {
        //weapon.Fire(firePoint);
    }

    /*private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D>();
    }*/
    
    private float GetAngle()
    {
        float ang;
        
        Vector2 direction = target.transform.position - firePoint.position;
        ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return ang;
    }

    private void SetAngle(float ang)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float min = startAngle - MAX_TURN;
        float max = startAngle + MAX_TURN;

        if (rotation.eulerAngles.z > max || rotation.eulerAngles.z < min)
        {
            rotation = Quaternion.AngleAxis(startAngle, Vector3.forward);
            shoot = false;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }

    public void Shoot() 
    {
        if (!shoot) return;

        if (lastCd > Time.time) return;
        
        lastCd = Time.time + (1f / fireRate);

        Bullet b = Instantiate(bullet, firePoint.position, firePoint.rotation);
        b.Fire(0);
    }

    public void SetFireRate(float fr)
    {
        fireRate = fr;
    }

    /*public void TakeDamage(int dmg)
    {
        Debug.Log("HP: " + hp);
        
        hp -= dmg;

        Debug.Log("DAMAGE: " + dmg);
        Debug.Log("HP: " + hp);
        
        if (hp == 0) Destroy(this);
    }*/

    public void OnDestroy()
    {
        bossFirstLayer.turrets.Remove(this);
        bossFirstLayer.UpdateFireRates();
    }
}
