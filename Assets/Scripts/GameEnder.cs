using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameEnder : MonoBehaviour
{
    public InputActionReference exitGameActionReference;
    public GameObject gameOverScreenPrefab; // Now, this is the prefab
    private GameObject currentGameOverScreen; // Reference to the instantiated object
    private TextMeshProUGUI gameOverText;

    private Timer timer;
    private InputAction exitGameAction;

    void Start()
    {
        timer = GetComponent<Timer>();
        if (timer == null)
        {
            Debug.LogError("GameEnder: Timer script not found on the same GameObject! GameEnder requires Timer component.");
        }

        // Instantiate the prefab.
        if (gameOverScreenPrefab != null)
        {
            currentGameOverScreen = Instantiate(gameOverScreenPrefab);

            // Find the text inside of the prefab.
            gameOverText = currentGameOverScreen.GetComponentInChildren<TextMeshProUGUI>(true); //true for looking in inactive objects.

            if (gameOverText == null)
            {
                Debug.LogError("GameEnder: GameOverText not found in the prefab.");
            }

            currentGameOverScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("GameEnder: Game Over screen prefab is not assigned in the Inspector!");
        }

        exitGameAction = exitGameActionReference.action;
        exitGameAction.Enable();
        exitGameAction.performed += OnExitGamePressed;
    }

    void OnDestroy()
    {
        if (exitGameAction != null)
        {
            exitGameAction.performed -= OnExitGamePressed;
            exitGameAction.Disable();
        }
        // Destroy the instantiated prefab when the script is destroyed.
        if (currentGameOverScreen != null)
        {
            Destroy(currentGameOverScreen);
        }
    }

    void Update()
    {
        if (timer != null && timer.TimeLeft <= 0)
        {
            EndGame();
        }
    }

    private void OnExitGamePressed(InputAction.CallbackContext context)
    {
        EndGame();
    }

    private void EndGame()
    {
        if (currentGameOverScreen != null)
        {
            currentGameOverScreen.SetActive(true);
            if (gameOverText != null)
            {
                gameOverText.text = "Game Ended";
            }
        }

        if (timer != null)
        {
            PlayerPrefs.SetFloat("TotalTime", timer.TotalTime);
            Debug.Log($"Game ended. Total time: {timer.TotalTime}");
        }
    }
}
