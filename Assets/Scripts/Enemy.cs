using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1f;
    public Transform[] points;
    public int enemyHealth = 5;
    private int currentHealth;

    public float knockbackForce = 5f;

    private SpriteRenderer spriteRenderer;

    private int i;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.25f )
        {
            i++;
            if(i== points.Length)
            {
                i= 0;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        spriteRenderer.flipX = (transform.position.x - points[i].position.x) < 0f;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, (ForceMode)ForceMode2D.Impulse);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


private void Die()
    {
       Destroy(gameObject);
    }
}
