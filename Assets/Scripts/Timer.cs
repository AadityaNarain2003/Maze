using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeLeft; // Internal variable for time left

    // Public property to get and set the timer value
    public float TimeLeft
    {
        get { return _timeLeft; }
        set { _timeLeft = Mathf.Max(0, value); } // Ensure time doesn't go below zero
    }

    // Constructor-like method to initialize the timer
    public void InitializeTimer(float startTime)
    {
        TimeLeft = startTime;
    }

    private void Update()
    {
        // Check if there's time left
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime; // Reduce time by seconds
            TimeLeft = Mathf.Max(0, TimeLeft); // Ensure it doesn't go below zero
        }
    }
    public void AddTime(float time)
    {
        Debug.Log("dfafewfafwef" + time);
        TimeLeft += time;
    }

    public void SubtractTime(float time)
    {
        Debug.Log("dfafewfafwef" + time);
        TimeLeft -= time;
        TimeLeft = Mathf.Max(TimeLeft, 0); // Clamp to zero to avoid negative time
    }
}
