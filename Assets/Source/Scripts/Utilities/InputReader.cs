using UnityEngine;
using System;

namespace Source.Scripts.Utilities
{
    public class InputReader : MonoBehaviour
    {
        private const KeyCode FlyKey = KeyCode.F;
        private const KeyCode ShootKey = KeyCode.K;
        private const KeyCode MenuKey = KeyCode.Escape;
        private const KeyCode ContinueKey = KeyCode.Space;
        
        public event Action FlyKeyPressed;
        public event Action ShootKeyPressed;
        public event Action ContinueKeyPressed;
        public event Action MenuKeyPressed;

        private void Update()
        {
            CheckFlyKeyInput();
            CheckShootKeyInput();
            CheckContinueKeyInput();
            CheckMenuKeyInput();
        }

        private void CheckFlyKeyInput()
        {
            if (Input.GetKeyDown(FlyKey))
            {
                FlyKeyPressed?.Invoke();
            }
        }
        
        private void CheckShootKeyInput()
        {
            if (Input.GetKeyDown(ShootKey))
            {
                ShootKeyPressed?.Invoke();
            }
        }
        
        private void CheckMenuKeyInput()
        {
            if (Input.GetKeyDown(MenuKey))
            {
                MenuKeyPressed?.Invoke();
            }
        }
        
        private void CheckContinueKeyInput()
        {
            if (Input.GetKeyDown(ContinueKey))
            {
                ContinueKeyPressed?.Invoke();
            }
        }
    }
}