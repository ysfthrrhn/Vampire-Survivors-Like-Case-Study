using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player : Character
    {
        public static Player Instance { get; private set; }
        
        [SerializeField]
        private PlayerMovement playerMovement;



        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            else
                Instance = this;
        }
        public override void OnDied()
        {
            playerMovement.CanMove = false;
            GameManager.Instance.StartCoroutine(GameManager.Instance.GameFinished());
            base.OnDied();
        }
        

    } 
}

