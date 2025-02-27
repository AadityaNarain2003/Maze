using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float _timeLeft; // Internal variable for time left (this is the game timer)
    private TextMeshProUGUI timerText; // Reference to the UI Text

    private float totalTime = 0; // Total time spent in the game
    public float timerBar; // Time needed to fill one star and reset health bar
    public float timerMax; // Maximum time allowed in the game (theoretical limit)
    private int totalStars;
    public Image healthBar; // Reference to the health bar UI element
    public GameObject starPrefab; // Prefab for the star UI element
    public Transform starsContainer; // Container for the stars

    // Public property to get and set the timer value (game timer)
    public float TimeLeft
    {
        get { return _timeLeft; }
        set
        {
            _timeLeft = Mathf.Max(0, value); // Ensure time doesn't go below zero
            UpdateTimerDisplay(); // Update UI when time changes
        }
    }

    // Constructor-like method to initialize the timer (game timer)
    public void InitializeTimer(float startTime)
    {
        TimeLeft = startTime;
    }

    public void setTMP()
    {
        StartCoroutine(WaitAndCheckObjects());
    }

    IEnumerator WaitAndCheckObjects()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second before checking

        int leftControllerLayer = LayerMask.NameToLayer("LeftController");

        if (leftControllerLayer == -1)
        {
            Debug.LogError("Layer 'Left Controller' not found! Make sure it exists in the Layers settings.");
            yield break;
        }

        // Find all GameObjects and check their layer
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == leftControllerLayer)
            {
                timerText = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (timerText != null) break;
            }
        }

        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI not found in Left Controller!");
        }
    }

    void Start()
    {
        totalStars = Mathf.FloorToInt(timerMax / timerBar); // Calculate total number of stars based on max time and bar time.
        Debug.Log($"Total stars: {totalStars}");
        CreateStars();
    }

    private void CreateStars()
    {
        for (int i = 0; i < totalStars; i++)
        {
            Instantiate(starPrefab, starsContainer);
        }
    }

    private void Update()
    {
        // Check if there's time left in the game
        if (TimeLeft > 0 && totalTime<timerMax)
        {
            float a = Time.deltaTime;
            TimeLeft -= a; // Reduce game time by delta time
            totalTime += a; // increase the total time.
            TimeLeft = Mathf.Max(0, TimeLeft); // Ensure it doesn't go below zero
        }
        UpdateTimerDisplay();
        UpdateHealthBar();
    }

    public void AddTime(float time)
    {
        TimeLeft += time;
    }

    public void SubtractTime(float time)
    {
        TimeLeft -= time;
        TimeLeft = Mathf.Max(TimeLeft, 0); // Clamp to zero to avoid negative time
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = $"{Mathf.FloorToInt(TimeLeft)}"; // Display time as an integer
        }
    }

    private void UpdateHealthBar()
    {
        // Calculate the time spent in the current "timerBar"
        float currentBarTime = totalTime % timerBar;

        //Calculate Health bar fill amount
        float progress = currentBarTime / timerBar;
        healthBar.fillAmount = progress;

        // Determine how many stars should be filled.
        int filledStars = Mathf.FloorToInt(totalTime / timerBar);

        //Update stars colors based on number of filled stars
        for (int i = 0; i < starsContainer.childCount; i++)
        {
            Image starImage = starsContainer.GetChild(i).GetComponent<Image>();
            starImage.color = i < filledStars ? Color.yellow : Color.gray;
        }
    }
}
