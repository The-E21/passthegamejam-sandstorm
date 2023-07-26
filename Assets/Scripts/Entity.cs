using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float maxHealth = 20;

    public float health = 0;

    [SerializeField] private string hurtSound;
    [SerializeField] private string deathSound;

    public virtual void Damage(float damage, Vector2 hitPos)
    {
        health += damage;
        if (health >= maxHealth)
        {
            Death();
        }
        else {
            AudioManager.Instance.Play(hurtSound);
        }
    }

    protected virtual void Death()
    {
        AudioManager.Instance.Play(deathSound);
        Destroy(gameObject);
    }
}
