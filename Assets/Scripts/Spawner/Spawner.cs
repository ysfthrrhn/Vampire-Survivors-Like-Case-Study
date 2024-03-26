using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField,Header("Spawn Distance")] protected float maxX1, maxX2, minX1, minX2, maxZ1, maxZ2, minZ1, minZ2;

        [SerializeField, Header("Spawn Offset")] protected Vector3 spawnOffset = Vector3.zero;
        protected Player _player;

        protected Vector3 currentSpawnPosition = Vector3.zero;

        [SerializeField] protected string poolName;

        protected virtual void Start()
        {
            _player = Player.Instance;
        }
        protected virtual void SpawnObjects()
        {
            // Calculate a random position outside the camera view
            currentSpawnPosition = GetRandomSpawnPosition();
            currentSpawnPosition.y = 0;
            currentSpawnPosition += spawnOffset;

            GameObject spawnedObject = ObjectPoolManager.Instance.GetPooledObject(poolName);
            spawnedObject.transform.position = currentSpawnPosition;
            spawnedObject.SetActive(true);

        }
        protected virtual Vector3 GetRandomSpawnPosition()
        {
            Vector3 playerPosition = _player.transform.position;

            // Randomly choose a side to spawn the enemy in x-z plane
            int sideIndex = Random.Range(0, 4); // 0: Top, 1: Bottom, 2: Left, 3: Right

            // Calculate a random position outside the player's view in x-z plane based on the chosen side
            Vector3 spawnPosition = Vector3.zero;

            switch (sideIndex)
            {
                case 0: // Top
                    spawnPosition = new Vector3(Random.Range(playerPosition.x - minX1, playerPosition.x + minX2), 0f, Random.Range(playerPosition.z + maxZ1, playerPosition.z + maxZ2));
                    break;
                case 1: // Bottom
                    spawnPosition = new Vector3(Random.Range(playerPosition.x - minX1, playerPosition.x + minX2), 0f, Random.Range(playerPosition.z - maxZ1, playerPosition.z - maxZ2));
                    break;
                case 2: // Left
                    spawnPosition = new Vector3(Random.Range(playerPosition.x - maxX1, playerPosition.x - maxX2), 0f, Random.Range(playerPosition.z - minZ1, playerPosition.z + minZ2));
                    break;
                case 3: // Right
                    spawnPosition = new Vector3(Random.Range(playerPosition.x + maxX1, playerPosition.x + maxX2), 0f, Random.Range(playerPosition.z - minZ1, playerPosition.z + minZ2));
                    break;
            }

            return spawnPosition;
        }
    }
}

