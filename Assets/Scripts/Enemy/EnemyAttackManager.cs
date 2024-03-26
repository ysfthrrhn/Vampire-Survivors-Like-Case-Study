using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyAttackManager : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private Enemy enemy;
        [SerializeField] private Transform handTransform;
        [SerializeField] private float animationTime = 4;

        private void Start()
        {
            _target = Player.Instance.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player") && enemy.GetPlayerState() == CharacterState.Running)
            {
                StartCoroutine(SetState());
            }
                
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && enemy.GetPlayerState() == CharacterState.Running)
            {
                StartCoroutine(SetState());
            }
        }

        private IEnumerator SetState()
        {
            enemy.SetPlayerState(CharacterState.Attack);
            yield return new WaitForSeconds(animationTime);
            enemy.SetPlayerState(CharacterState.Running);
        }

        public void ShootBall()
        {
            GameObject ball = ObjectPoolManager.Instance.GetPooledObject("Ball");
            ball.GetComponent<Ball>().SetTarget(_target);
            ball.transform.position = handTransform.position;
            ball.SetActive(true);
        }
    }
}

