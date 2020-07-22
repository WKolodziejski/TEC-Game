using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Character : MonoBehaviour
{

    public Weapon weaponPrefab;
    public float hp = 3f;
    public float movementSpeed = 5f;
    public float damageCooldown = 0f;

    protected Weapon weapon;
    protected Animator animator;
    protected Rigidbody2D rb;
    
    private Action onDie;
    private Action onDamage;
    private float lastCooldown;
    private bool isDead;

    //Inicializa componetes comuns
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //SetWeapon(weaponPrefab);
        InitializeComponents();
    }

    //Inicializa componentes específicos
    protected abstract void InitializeComponents();

    private void SetWeapon(Weapon prefab)
    {
        if (weapon != null)
            Destroy(weapon.gameObject);

        weapon = Instantiate(prefab, transform);
    }

    protected void Fire(Transform barrel)
    {
        weapon.Fire(barrel);
    }

    public virtual void TakeDamage(float damage)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + damageCooldown;

            hp -= damage;

            OnDamage(damage);
            
            if (hp <= 0)
            {
                Kill();
            }
        }
    }
    
    public void Kill()
    {
        if (!isDead)
        {
            hp = 0;
            isDead = true;

            OnDie(); 
        }
    }

    //Listeners INTERNOS

    protected virtual void OnDie()
    {
        onDie?.Invoke();

        animator.SetTrigger("OnDie");
    }

    protected virtual void OnDamage(float damage)
    {
        onDamage?.Invoke();
    }

    //Listeners EXTERNOS

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void SetOnDamageListener(Action onDamage)
    {
        this.onDamage = onDamage;
    }

    public bool IsDead()
    {
        return isDead;
    }

}
