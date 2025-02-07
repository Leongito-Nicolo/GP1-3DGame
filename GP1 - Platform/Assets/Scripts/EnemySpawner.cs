using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Vector3> enemySpawnPoints = new();        // list of all spwanpoints of enemies
    [SerializeField] private GameObject enemy;        // enemy prefab

    [SerializeField] private GameManager gameManager;       // instance of game manager

    public void SpawnEnemies()
    {
        for (int i = 0; i < gameManager.totalEnemies; i++)
        {
            Instantiate(enemy, enemySpawnPoints[i], enemy.transform.rotation, transform); // create the physical object
        }
    }
}
