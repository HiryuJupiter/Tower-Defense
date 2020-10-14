using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Managers;

namespace TowerDefence.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        float spawnRate = 1f;

        float spawnTimer = 0f;

        //Class reference
        EnemyManager enemyManager;

        //Properties
        public float SpawnRate => spawnRate;

        void Start()
        {
            enemyManager = EnemyManager.instance;
            enemyManager?.SpawnEnemy(transform);
        }

        void Update()
        {
            //TickSpawningTimer();
        }

        void TickSpawningTimer()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > SpawnRate)
            {
                spawnTimer = 0f;
                enemyManager?.SpawnEnemy(transform);
            }
        }
    }
}