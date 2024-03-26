using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Coin : MonoBehaviour ,IPoolObject
    {
        public void Collect()
        {
            GameManager.Instance.UpdateCoin();
            ReturnToPool();
        }
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}

