using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager Instance;

        public List<Transform> targets = new List<Transform>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
                if(other.GetComponent<Enemy>().CurrentCharacterState != CharacterState.Death)
                    targets.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
                targets.Remove(other.transform);
        }

        public Transform GetTarget()
        {
            if (targets.Count == 0)
                return null;

            Transform nearestTarget = targets[0];
            float nearestDistance = float.MaxValue;
            
            foreach (Transform target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance < nearestDistance)
                {
                    nearestTarget = target;
                    nearestDistance = distance;
                }
            }

            return nearestTarget;
        }

    }
}

