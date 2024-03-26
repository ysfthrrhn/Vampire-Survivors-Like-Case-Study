using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class PlayerAttackManager : MonoBehaviour
    {
        public bool CanAttack = true;

        [SerializeField] private TargetManager targetManager;

        [SerializeField, Range(0f, 2f)]
        private float cooldown = 1f;
        private float _remainingCooldown = 0f;

        [SerializeField]
        private Vector3 firingOffset = Vector3.zero;

        void Update()
        {
            _remainingCooldown -= Time.deltaTime;

            // Check if it's time to fire based on the remaining cooldown
            if (_remainingCooldown <= 0f && targetManager.GetTarget() != null)
            {
                FireBoomerang();
                // Reset the remaining cooldown to the full cooldown duration
                _remainingCooldown = cooldown;
            }
        }

        private void FireBoomerang()
        {
            Transform target = targetManager.GetTarget();

            GameObject boomerang = ObjectPoolManager.Instance.GetPooledObject("Boomerang");
            boomerang.SetActive(true);
            boomerang.transform.position = transform.position + firingOffset;
            boomerang.GetComponent<Boomerang>().SetTarget(target);
        }
    }
}

