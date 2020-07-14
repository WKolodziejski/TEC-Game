using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private Rigidbody2D rb;

    /*public void setVelocity(float horizontalMultiplier,
                            float verticalMultiplier)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * horizontalMultiplier, speed * verticalMultiplier);
    }*/

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy" && gameObject.tag == "PlayerBullet")
            hit(collisionInfo.collider.gameObject);
        else if (collisionInfo.collider.tag == "Player" && gameObject.tag == "EnemyBullet")
            hit(collisionInfo.collider.gameObject);
        Destroy(gameObject);
    }

    public void hit(GameObject collider)
    {
        HackingCharacter character = collider.GetComponent<HackingCharacter>();

        character.takeDamage(damage);
    }

}