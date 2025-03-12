using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitcher : MonoBehaviour
{
    public InputActionProperty xButtonAction; // For X button
    public InputActionProperty yButtonAction; // For Y button

    void Update()
    {
        if (xButtonAction.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene("GameTemplate");
        }

        if (yButtonAction.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene("StaticMap");
        }
    }
}
