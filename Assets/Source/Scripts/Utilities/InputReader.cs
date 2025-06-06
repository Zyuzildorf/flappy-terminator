using UnityEngine;
using System;

namespace Source.Scripts.Utilities
{
    public class InputReader : MonoBehaviour
    {
        private const KeyCode KeyF = KeyCode.F;
        private const KeyCode KeyK = KeyCode.K;
        private const KeyCode KeyEscape = KeyCode.Escape;
        private const KeyCode KeySpaceBar = KeyCode.Space;
        
        public event Action KeyFPressed;
        public event Action KeyKPressed;
        public event Action SpaceBarPressed;
        public event Action KeyEscapePressed;

        private void Update()
        {
            CheckKeyFInput();
            CheckKeyKInput();
            CheckSpaceBarInput();
            CheckKeyEscapeInput();
        }

        private void CheckKeyFInput()
        {
            if (Input.GetKeyDown(KeyF))
            {
                KeyFPressed?.Invoke();
            }
        }
        
        private void CheckKeyKInput()
        {
            if (Input.GetKeyDown(KeyK))
            {
                KeyKPressed?.Invoke();
            }
        }
        
        private void CheckKeyEscapeInput()
        {
            if (Input.GetKeyDown(KeyEscape))
            {
                KeyEscapePressed?.Invoke();
            }
        }
        
        private void CheckSpaceBarInput()
        {
            if (Input.GetKeyDown(KeySpaceBar))
            {
                SpaceBarPressed?.Invoke();
            }
        }
    }
}