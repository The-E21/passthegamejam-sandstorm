using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevGio;

[CreateAssetMenu(menuName = "Abilities/Projectile Ability")]
public class ProjectileAbility : Ability
{
    public Projectile bullet;
    public int bulletWaveAmount = 1;
    [Tooltip("Repeat fire on top of original firing")] 
    public int repeatAmount = 0;
    
    public float burstCooldown = 0.1f;
    public float angleOffset = 0;
    [Tooltip("Offset waveAngle by player aim")] 
    public bool usePlayerAngle = true;
    
    public float waveSpread = 5;
    [Tooltip("Spread bullets over this arc")] 
    public float multipleWaveSpread = 90;
    public bool fullCircleSpread = false;
    [Tooltip("Use multipleBulletSpread to evenly separate multiple bullets")] 
    public bool evenWaveSpread = true;
    
    public Vector2[] waveSpawnPoints;
    public Vector2 waveSpawnOffset = Vector2.right;
    [Tooltip("Whether bullets in a wave will use the waves waveAngle or their waveAngle from the spawn offset")] 
    public bool useRelativeAngle = true;

    public bool rotateWavePoints = true;

    private bool isFiringBurst;
    private int burstCount;
    private float burstTimer;

    public override void Update()
    {
        base.Update();

        if (isFiringBurst)
        {
            burstTimer += Time.deltaTime;
            if (burstTimer >= burstCooldown)
            {
                burstTimer -= burstCooldown;
                burstCount++;
                Fire();

                if (burstCount >= repeatAmount)
                    isFiringBurst = false;
            }
        }
    }

    protected override void Function()
    {
        Fire();
        if (repeatAmount > 0)
        {
            isFiringBurst = true;
            burstCount = 0;
            burstTimer = 0;
        }
    }

    protected void Fire()
    {
        Vector2 startPos = GameObject.Find("Player").transform.position;
        for (int i = 0; i < bulletWaveAmount; i++)
        {
            float waveAngle = Random.Range(-waveSpread, waveSpread) * 0.5f + angleOffset;
            if (usePlayerAngle)
                waveAngle += abilityHandler.aimAngle;

            if (bulletWaveAmount > 1 && evenWaveSpread)
            {
                if (fullCircleSpread)
                {
                    waveAngle += 360f / bulletWaveAmount * i;
                }
                else
                {
                    waveAngle += multipleWaveSpread / (bulletWaveAmount - 1) * i;
                    waveAngle -= multipleWaveSpread / 2;
                }
            }

            Vector2 waveOffset = waveSpawnOffset.Rotated(waveAngle);
            Vector2 spawnPos = waveOffset + startPos;
            for (int b = 0; b < waveSpawnPoints.Length; b++)
            {
                Vector2 spawnOffset = rotateWavePoints ? waveSpawnPoints[b].Rotated(waveAngle) : waveSpawnPoints[b];
                float angle = waveAngle;
                if (useRelativeAngle)
                {
                    angle = HelperMethods.VectorToDeg(spawnOffset);
                }

                Vector2 direction = angle.ToVector();

                var p = Instantiate(bullet, spawnPos + spawnOffset, Quaternion.identity);
                p.Build(direction);
            }

            if (waveSpawnPoints == null || waveSpawnPoints.Length <= 0)
            {
                var p = Instantiate(bullet, spawnPos, Quaternion.identity);
                p.Build(waveAngle.ToVector());
            }
        }
    }
}
