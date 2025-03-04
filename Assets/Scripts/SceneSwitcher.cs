using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public InputActionProperty switchSceneAction; // Drag your Input Action here from the Inspector
    public string sceneToLoad; // Name of the scene to load

    void OnEnable()
    {
        Debug.Log("SceneSwitcher enabled");
        switchSceneAction.action.Enable();
        switchSceneAction.action.performed += OnSwitchScenePressed; // Subscribe to the performed event
    }

    void OnDisable()
    {
        switchSceneAction.action.performed -= OnSwitchScenePressed; // Unsubscribe when disabled
        switchSceneAction.action.Disable();
    }

    private void OnSwitchScenePressed(InputAction.CallbackContext context)
    {
        SwitchScene();
    }

    private void SwitchScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad); // Replace with the desired scene name
        Debug.Log($"Switching to scene: {sceneToLoad}");
    }
}