using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class PlayerCollisions : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            switch (other.transform.tag)
            {
                case "Coin":
                    other.transform.GetComponent<Coin>().Collect();
                    break;
                case "Enemy":
                    GetComponent<PlayerHealth>().TakeDamage(200);
                    break;
                //case "Ball":
                //    other.transform.GetComponent<Ball>().ReturnToPool();
                //    break;
                case null: break;
            }

        }
    }
}

