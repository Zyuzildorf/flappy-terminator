using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool IsSpacebarPressed { get; private set; }
    public bool IsFKeyPressed { get; private set; }

    private void Update()
    {
        UpdateSpaceBarInput();
        UpdateFKeyInput();
    }

    private void UpdateSpaceBarInput()
    {
        IsSpacebarPressed = Input.GetKeyDown(KeyCode.F);
    }

    private void UpdateFKeyInput()
    {
        IsFKeyPressed = Input.GetKeyUp(KeyCode.K);
    }
}
