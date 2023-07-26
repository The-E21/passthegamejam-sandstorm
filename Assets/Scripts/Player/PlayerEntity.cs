using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public float hitCooldown = 0.5f;
    public ProgressBar healthBar;
    public float knockbackForce = 5;

    private Rigidbody2D rb;
    private bool isVulnerable = true;
    private float hitTimer = 0; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isVulnerable)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitCooldown)
            {
                isVulnerable = true;
            }
        }

        healthBar.SetValue(1f - health / maxHealth);
    }

    public override void Damage(float damage, Vector2 hitPos)
    {
        if (isVulnerable)
        {
            base.Damage(damage, hitPos);
            isVulnerable = false;
            hitTimer = 0;

        }
        Vector2 dir = (Vector2)transform.position - hitPos;
        rb.AddForce(dir.normalized * knockbackForce * (isVulnerable ? 1 : 0.5f), ForceMode2D.Impulse);
    }

    protected override void Death()
    {
        base.Death();

        DeathHandler.Instance.StartDeath();
    }
}
