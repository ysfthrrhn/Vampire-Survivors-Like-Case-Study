using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerAnimations : MonoBehaviour
    {
        public Animator characterAnimator;

        void Start()
        {
            Player.Instance.StateChanged.AddListener(delegate { ChangeAnimation(); });
        }

        private void ChangeAnimation()
        {
            switch (Player.Instance.GetPlayerState())
            {
                case CharacterState.Idle:
                    characterAnimator.SetTrigger("Idle");
                    break;

                case CharacterState.Running:
                    characterAnimator.SetTrigger("Run");
                    break;

                case CharacterState.Death:
                    characterAnimator.SetTrigger("Death");
                    this.enabled = false;
                    break;
                case CharacterState.Attack:
                    break;
            }



        }

        private void OnDisable()
        {
            Player.Instance.StateChanged.RemoveAllListeners();
        }
    }
}

