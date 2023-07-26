using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public static DeathHandler Instance;

    [SerializeField] private string deathScreen;
    public EnemySpawner enemySpawner;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDeath()
    {
        UIHandler.Instance.SwapTo(deathScreen);
        enemySpawner.enabled = false;
    }
}
