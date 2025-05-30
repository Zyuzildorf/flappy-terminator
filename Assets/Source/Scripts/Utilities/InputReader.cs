using UnityEngine;

namespace Source.Scripts.Utilities
{
    public class InputReader : MonoBehaviour
    {
        public bool IsKeyFPressed { get; private set; }
        public bool IsKeyKPressed { get; private set; }
        public bool IsSpaceBarPressed { get; private set; }
        public bool IsEscapeKeyPressed { get; private set; }

        private void Update()
        {
            UpdateKeyFInput();
            UpdateKeyKInput();
            UpdateSpaceBarInput();
            UpdateEscapeKeyInput();
        }

        private void UpdateKeyFInput()
        {
            IsKeyFPressed = Input.GetKeyDown(KeyCode.F);
        }

        private void UpdateKeyKInput()
        {
            IsKeyKPressed = Input.GetKeyDown(KeyCode.K);
        }

        private void UpdateSpaceBarInput()
        {
            IsSpaceBarPressed = Input.GetKeyUp(KeyCode.Space);
        }

        private void UpdateEscapeKeyInput()
        {
            IsEscapeKeyPressed = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}