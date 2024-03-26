using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game
{
    public class Ball : Projectile, IPoolObject
    {
        [SerializeField] private int damage = 30;
        [SerializeField] private float duration = 3;
        [SerializeField] private Rigidbody rb;

        protected override void MoveToTarget()
        {
            
            Vector3 moveDirection = (_target.position - transform.position);
            moveDirection.y = 0;
            moveDirection.Normalize();
            StartCoroutine(StartToMove(moveDirection));
        }

        private IEnumerator StartToMove(Vector3 direction)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                rb.velocity = direction * moveSpeed;
                elapsed += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector3.zero;
            ReturnToPool();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                if (other.CompareTag("Player"))
                    other.GetComponent<PlayerHealth>().TakeDamage(damage);
                ReturnToPool();
            }
        }
        private void OnEnable()
        {
            if(_target != null)
                MoveToTarget();
            else
                ReturnToPool();
        }
    }
}

