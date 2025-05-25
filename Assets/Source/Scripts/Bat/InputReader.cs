using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool IsKeyFPressed { get; private set; }
    public bool IsKeyKPressed { get; private set; }

    private void Update()
    {
        UpdateKeyFInput();
        UpdateKeyKInput();
    }

    private void UpdateKeyFInput()
    {
        IsKeyFPressed = Input.GetKeyDown(KeyCode.F);
    }

    private void UpdateKeyKInput()
    {
        IsKeyKPressed = Input.GetKeyUp(KeyCode.K);
    }
}