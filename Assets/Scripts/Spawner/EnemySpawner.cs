using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private float spawnInterval = 3f;
        
        protected override void Start()
        {
            base.Start();
            InvokeRepeating(nameof(SpawnObjects), 0f, spawnInterval);
        }
    }
}

