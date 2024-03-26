using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CoinSpawner : Spawner
    {
        [SerializeField] private int initialCoinCount = 100;

        protected override void Start()
        {
            base.Start();
            int i = 0;

            while (i < initialCoinCount)
            {
                SpawnObjects();
                i++;
            }
        }
    }
}

