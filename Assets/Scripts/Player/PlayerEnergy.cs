using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 30;
    public float energyRechargeRate = 10;
    public float rechargeCooldown = 0.25f;
    public ParticleSystem particles;
    public float energyToParticles = 0.5f;
    public ProgressBar energyBar;

    public float energy;

    private bool isRecharging = false;
    private float rechargeTimer = 0;

    private void Awake()
    {
        energy = maxEnergy;
    }

    private void Update()
    {
        if (!isRecharging)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= rechargeCooldown)
            {
                rechargeTimer -= rechargeCooldown;
                isRecharging = true;
            }
        }
        else
        {
            energy += energyRechargeRate * Time.deltaTime;
            ClampEnergy();
        }

        var emission = particles.emission;
        emission.rateOverTime = Mathf.CeilToInt(energy * energyToParticles);
        
        energyBar.SetValue(energy / maxEnergy);
    }

    public void UseEnergy(float amount)
    {
        energy -= amount;
        isRecharging = false;
        rechargeTimer = 0;
        ClampEnergy();
    }

    public bool HasEnergy(float amount) => energy >= amount;

    private void ClampEnergy() => energy = Mathf.Clamp(energy, 0, maxEnergy);
}
