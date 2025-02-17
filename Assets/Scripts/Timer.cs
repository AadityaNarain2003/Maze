using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timeLeft; // Internal variable for time left
    private TextMeshProUGUI timerText; // Reference to the UI Text

    private float totalTime=0;

    // Public property to get and set the timer value   
    public float TimeLeft
    {
        get { return _timeLeft; }
        set 
        { 
            _timeLeft = Mathf.Max(0, value); // Ensure time doesn't go below zero
            UpdateTimerDisplay(); // Update UI when time changes
        }
    }

    // Constructor-like method to initialize the timer
    public void InitializeTimer(float startTime)
    {
        TimeLeft = startTime;
    }

    public void setTMP()
    {
        StartCoroutine(WaitAndCheckObjects());
        //this.timerText = timerText;
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
                //Debug.Log($"Object in 'Left Controller' layer: {obj.name}");
                timerText = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (timerText != null) break;
            }
        }

        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI not found in Left Controller!");
        }
    }

    private void Update()
    {
        // Check if there's time left
        if (TimeLeft > 0)
        {
            float a=Time.deltaTime;
            TimeLeft -= a; // Reduce time by seconds
            totalTime +=a;
            TimeLeft = Mathf.Max(0, TimeLeft); // Ensure it doesn't go below zero
        }
        UpdateTimerDisplay();
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
            timerText.text = $"{TimeLeft:F2}"; // Display time with 2 decimal places
        }
    }
}
