using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove = true;

        [SerializeField]
        private FloatingJoystick joystick;
        
        [SerializeField,Range(0,15)]
        private float moveSpeed = 10f;

        private bool isRunning = false;

        // Update is called once per frame
        void FixedUpdate()
        {
            if(joystick.Direction != Vector2.zero && CanMove)
            {
                if(!isRunning)
                {
                    Player.Instance.SetPlayerState(CharacterState.Running);
                    isRunning = true;
                }
                HandleMovementInput();
            }
            else if(CanMove)
            {
                if (isRunning)
                {
                    Player.Instance.SetPlayerState(CharacterState.Idle);
                    isRunning = false;
                }
            }
            else
            {
                joystick.gameObject.SetActive(false);// Turning off when died
                this.enabled = false;
            }
                
            
        }

        void HandleMovementInput()
        {
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            
            // Calculate movement direction (X and Z axes only)
            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;


            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 400 * Time.deltaTime);
            }


            // Apply look direction
            //transform.rotation = Quaternion.LookRotation(joystick.Direction);

            // Apply movement speed (X and Z axes only)
            Vector3 moveVector = moveDirection * moveSpeed * Time.deltaTime;
            
            transform.position += moveVector;
        }

    }
}

