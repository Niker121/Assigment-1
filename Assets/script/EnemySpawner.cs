using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; // Prefab c?a quái
        public Transform spawnPoint; // ?i?m spawn
        public float spawnInterval = 5f; // Th?i gian gi?a các l?n spawn

        private void Start()
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }

        void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

