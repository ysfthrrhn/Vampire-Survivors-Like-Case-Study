using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyAnimations : MonoBehaviour
    {
        public Animator characterAnimator;
        
        [SerializeField]
        private Enemy enemy;

        void Start()
        {
            enemy.StateChanged.AddListener(delegate { ChangeAnimation(); });
        }

        private void ChangeAnimation()
        {
            switch (enemy.GetPlayerState())
            {
                case CharacterState.Idle:
                    break;

                case CharacterState.Running:
                    enemy.enemyMovement.CanMove = true;
                    characterAnimator.SetTrigger("Run");
                    break;

                case CharacterState.Death:
                    characterAnimator.SetTrigger("Death");
                    this.enabled = false;
                    break;
                case CharacterState.Attack:
                    enemy.enemyMovement.CanMove = false;
                    characterAnimator.SetTrigger("Attack");
                    break;
            }



        }
    }
}

