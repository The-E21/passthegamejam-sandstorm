using DevGio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector2 spawnRange = new Vector2(4, 10);
    public GameObject enemy;
    public Transform playerTransform;
    public float spawnCooldown = 1;

    private float spawnTimer;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerEntity>().transform;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnCooldown)
        {
            spawnTimer -= spawnCooldown;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 pos = playerTransform.position;
        Vector2 offset = HelperMethods.DegToVector(Random.Range(0, 360f)) * Random.Range(spawnRange.x, spawnRange.y);

        Instantiate(enemy, pos + offset, Quaternion.identity);
    }
}
