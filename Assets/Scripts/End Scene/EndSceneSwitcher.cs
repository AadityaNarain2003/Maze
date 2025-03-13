using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndSceneSwitcher : MonoBehaviour
{
    public InputActionProperty xButtonAction; // For B button on right controller
    private Timer timer;

    void Start()
    {
        // Get the Timer component attached to the same GameObject.
        timer = GetComponent<Timer>();

        if (timer == null)
        {
            Debug.LogError("EndSceneSwitcher: Timer script not found on the same GameObject! Please attach the Timer component to this GameObject.");
        }
    }

    void Update()
    {
        if (xButtonAction.action.WasPressedThisFrame())
        {
            SwitchToEndScene();
        }
    }

    private void SwitchToEndScene()
    {
        if (timer != null)
        {
            // Save the total time in PlayerPrefs.
            PlayerPrefs.SetFloat("TotalTime", timer.TotalTime);
            Debug.Log($"EndSceneSwitcher: Saved total time: {timer.TotalTime} to PlayerPrefs.");
        }
        else
        {
            Debug.LogWarning("EndSceneSwitcher: Timer script not found, TotalTime not saved.");
        }

        // Load the End Scene.
        SceneManager.LoadScene("End Scene");
    }
}
