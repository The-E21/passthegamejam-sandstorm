using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float cooldownTime = 1;
    public float energyUsage = 1;

    public bool isReady;

    private float cdTimer = 0;
    protected PlayerAbility abilityHandler;

    public void SetParent(PlayerAbility abilityHandler) => this.abilityHandler = abilityHandler;

    public virtual void Update()
    {
        if (!isReady)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer >= cooldownTime)
            {
                cdTimer -= cooldownTime;
                isReady = true;
            }
        }
    }

    public virtual void FixedUpdate() {}

    public void Trigger()
    {
        isReady = false;
        Function();
    }

    protected abstract void Function();
}
