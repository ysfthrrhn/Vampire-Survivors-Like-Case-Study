using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class EnemyMovement : MonoBehaviour
    {
        public bool CanMove;

        [SerializeField, Range(0, 15)]
        private float moveSpeed = 5f;

        private Transform target;

        void Start()
        {
            target = Player.Instance?.transform;
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            // Check if the player reference is not null
            if (target != null)
            {
                // Calculate the direction vector from the enemy to the player
                Vector3 direction = (target.position - transform.position).normalized;

                // Ignore the Y component, set Y to zero
                direction.y = 0f;

                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 400 * Time.deltaTime);

                if (CanMove)
                {
                    Vector3 moveVector = direction * moveSpeed * Time.deltaTime;
                    transform.position += moveVector;
                }

            }
            else
                target = Player.Instance?.transform;
        }
    }
}

