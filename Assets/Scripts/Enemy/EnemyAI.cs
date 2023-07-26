using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float movementSpeed = 80;
    public float damage = 5;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 direction;

    private bool targetDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (target)
        {
            direction = target.position - transform.position;
            direction.Normalize();
        }
        else if (!targetDead)
        {
            targetDead = true;
            direction *= -1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity += direction * (movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerEntity player))
        {
            player.Damage(damage, transform.position);
        }
    }
}
