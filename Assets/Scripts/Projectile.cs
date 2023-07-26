using DevGio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask hitMask;
    public float speed = 10;
    public float damage = 2;
    public float lifeTime = 10;
    public GameObject deathParticles;

    private float timer = 0;
    private Rigidbody2D rb;
    private Vector2 direction;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        rb.velocity = direction * speed;
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HelperMethods.IsOnLayer(hitMask, collision.gameObject.layer))
        {
            OnHit(collision.gameObject);
        }
    }

    protected virtual void OnHit(GameObject collided)
    {
        if (collided.TryGetComponent(out IDamageable damagable))
        {
            damagable.Damage(damage, transform.position);
        }

        Death();
    }

    protected virtual void Death()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Build(Vector2 direction)
    {
        this.direction = direction;
    }
}
