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
        int rightControllerLayer = LayerMask.NameToLayer("RightController");

        if (leftControllerLayer == -1 || rightControllerLayer == -1)
        {
            Debug.LogError("Layer 'Left Controller' or 'Right Controller' not found! Make sure they exist in the Layers settings.");
            yield break;
        }

        // Find all GameObjects and check their layer
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == leftControllerLayer)
            {
                timerText = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (timerText != null)
                {
                  
                }
            }
            if (obj.layer == rightControllerLayer)
            {
              
                foreach (Image img in obj.GetComponentsInChildren<Image>(true))
                {
                    if (img.name == "HealthBar")
                    {
                        healthBar=img;
                    }
                }
                foreach (Transform tr in obj.GetComponentsInChildren<Transform>(true))
                {
                    if (tr.name == "StarsContainer")
                    {
                        starsContainer=tr;
                    }
                }
            }
        }

        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI not found in Left Controller!");
        }
        if (healthBar == null)
        {
            Debug.LogError("HealthBar not found in Right Controller!");
        }
        if (starsContainer == null)
        {
            Debug.LogError("StarsContainer not found in Right Controller!");
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
        if (starsContainer == null)
        {
            Debug.LogError("StarsContainer is null. Cannot create stars.");
            return;
        }
        for (int i = 0; i < totalStars; i++)
        {
            // Instantiate the star as a child of the starsContainer.
            GameObject newStar = Instantiate(starPrefab, starsContainer);

            // Get the RectTransform
            RectTransform starRectTransform = newStar.GetComponent<RectTransform>();
            if (starRectTransform != null)
            {
                // Set the anchor presets to middle-left
                starRectTransform.anchorMin = new Vector2(0, 0.5f);
                starRectTransform.anchorMax = new Vector2(0, 0.5f);

                // Set the pivot to the left
                starRectTransform.pivot = new Vector2(0, 0.5f);

                // Reset the local position and scale
                starRectTransform.localPosition = Vector3.zero;
                starRectTransform.localScale = Vector3.one;
                starRectTransform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogError("Missing RectTransform component on star prefab!");
            }
        }
    }

    private void Update()
    {
        // Check if there's time left in the game
        if (TimeLeft > 0 && totalTime < timerMax)
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

    private void OnTransformParentChanged()
    {
        Debug.LogWarning($"A star's parent has changed! Current parent: {transform.parent.name}");
    }
    private void UpdateHealthBar()
    {
        Debug.Log("stars container is: "+starsContainer);
        if (healthBar==null)
        {
            return;
        }
        // Calculate the time spent in the current "timerBar"
        float currentBarTime = totalTime % timerBar;

        //Calculate Health bar fill amount
        float progress = currentBarTime / timerBar;
        healthBar.fillAmount = progress;

        // Determine how many stars should be filled.
        int filledStars = Mathf.FloorToInt(totalTime / timerBar);
        Debug.Log("Filled Stars: "+filledStars);
        //Update stars colors based on number of filled stars
        if (starsContainer!=null)
        {
            Debug.Log(" total children: "+starsContainer.childCount);
            Debug.Log("stars container is: "+starsContainer);
            for (int i = 0; i < starsContainer.childCount; i++)
            {
                Image starImage = starsContainer.GetChild(i).GetComponent<Image>();
                Debug.Log("Star Changed");  
                Debug.Log("Colour of star "+i+" is "+(i < filledStars ? Color.yellow : Color.gray));
                Debug.Log("Filled Stars: "+filledStars);

                starImage.color = i < filledStars ? Color.yellow : Color.gray;
            }
        }
        
    }
}
