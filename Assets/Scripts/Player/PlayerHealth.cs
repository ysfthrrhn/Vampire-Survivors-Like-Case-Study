using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private Image healthBar;

        [SerializeField, Range(0, 100)]
        private int health = 100;
        private int _currentHealth;

        private bool _isDead = false;

        private Transform _camera;
        private Transform _healthBarTransform;

        private void Awake()
        {
            _healthBarTransform = healthBar.transform.parent; // Getting health bar's parent to face Camera after
            _camera = Camera.main.transform;
            _currentHealth = health;           
        }

        private void Update()
        {
            FaceToCamera();
        }

        public void TakeDamage(int damage)
        { 
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Player.Instance.OnDied();
                _isDead = true;
            }
                
                
            StartCoroutine(SetHealthBar(_isDead));
        }

        public IEnumerator SetHealthBar(bool isDead = false)
        {
            float targetValue = (float)_currentHealth / health;
            
            if(targetValue < 0)
                targetValue = 0;

            while ((healthBar.fillAmount - targetValue) > 0.03f)
            {
                // Smoothly interpolate towards the target value
                healthBar.fillAmount -= Time.deltaTime;

                yield return null;
            }

            healthBar.fillAmount = targetValue;

            if (isDead)
            {
                yield return new WaitForSeconds(.3f);
                healthBar.transform.parent.gameObject.SetActive(false);
            }
            

        }
        private void FaceToCamera()
        {
            Vector3 lookPos = new Vector3(_healthBarTransform.position.x, _camera.position.y, _camera.position.z);
            _healthBarTransform.LookAt(lookPos); // Facing health bar to Camera
        }
    }
}

