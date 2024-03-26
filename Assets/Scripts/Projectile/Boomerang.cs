using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game
{
    public class Boomerang : Projectile, IPoolObject
    {
        private void FixedUpdate()
        {
            if (_target != null)
                MoveToTarget();
        }
        protected override void MoveToTarget()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, moveSpeed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().OnDied();
                ReturnToPool();
            }
        }

    }
}

