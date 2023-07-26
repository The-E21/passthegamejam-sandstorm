using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [SerializeField] private int exp;
    [SerializeField] private GameObject scoreParticle;

    protected override void Death() {
        ExperianceManager.Instance.GainExp(exp);
        ScoreParticle particles = Instantiate(scoreParticle).GetComponent<ScoreParticle>();
        particles.score = exp.ToString();
        particles.transform.position = transform.position;
        base.Death();
    }
}
