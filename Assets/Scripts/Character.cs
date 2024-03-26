using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Character : MonoBehaviour
    {
        public CharacterState CurrentCharacterState;

        [HideInInspector]
        public UnityEvent StateChanged = new UnityEvent();

        public virtual void OnDied() 
        {
            SetPlayerState(CharacterState.Death);
        }

        public void SetPlayerState(CharacterState state)
        {
            CurrentCharacterState = state;
            StateChanged.Invoke();
        }

        public CharacterState GetPlayerState() { return CurrentCharacterState; }
    }
}

