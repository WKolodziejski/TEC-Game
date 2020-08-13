using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public Weapon weaponPrefab;
    public Transform mainBarrel;
    public DamagePopup damagePopup;

    public float maxHP = 3f;
    public float movementSpeed = 5f;
    public float damageCooldown = 0f;

    protected Weapon weapon;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected float hp;

    private List<Action> onDie;
    private List<Action> onDamage;
    private float lastCooldown;
    private bool isDead;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        hp = maxHP;

        if (weaponPrefab != null)
            SetWeapon(weaponPrefab);

        InitializeComponents();
    }

    //Inicializa componentes específicos
    protected abstract void InitializeComponents();

    public void SetWeapon(Weapon prefab)
    {
        if (weapon != null)
            Destroy(weapon.gameObject);

        weapon = Instantiate(prefab, transform);
    }

    //tive q botar essa flag pq ao retornar do hack pode ser q n tome o dano, caso um inimigo atire antes
    public virtual void TakeDamage(float damage, bool force)
    {
        if (lastCooldown <= Time.time || force)
        {
            lastCooldown = Time.time + damageCooldown;

            hp -= damage;

            OnDamage(damage);
            
            if (hp <= 0)
                Kill();
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

    public float GetHP()
    {
        return hp;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetEnabled(bool enabled)
    {
        if (!isDead)
            this.enabled = enabled;
    }

    //Listeners INTERNOS
    //CHAMAR BASE.METODO()!!!!!!!!!!!!!!!!!!!!!!!!!!!

    protected virtual void OnDie()
    {
        enabled = false;

        StopAllCoroutines();

        CallOnDie();

        animator.SetBool("dead", isDead);

        Destroy(gameObject, 2f);
    }

    protected virtual void OnDamage(float damage)
    {
        if (damagePopup != null)
            Instantiate(damagePopup, transform.position, Quaternion.identity).Hit(-damage);

        CallOnDamage();
    }

    //Listeners EXTERNOS

    public void SetOnDieListener(Action a)
    {
        if (onDie == null)
            onDie = new List<Action>();

        onDie.Add(a);
    }

    public void SetOnDamageListener(Action a)
    {
        if (onDamage == null)
            onDamage = new List<Action>();

        onDamage.Add(a);
    }

    private void CallOnDie()
    {
        if (onDie != null)
            foreach (Action a in onDie)
                a?.Invoke();
    }

    private void CallOnDamage()
    {
        if (onDamage != null)
            foreach (Action a in onDamage)
                a?.Invoke();
    }

}
