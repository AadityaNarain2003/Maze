using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticPlayerManager : MonoBehaviour
{
    private Player player;
    // private Timer timer; // Keep it private

    public int coin_add;
    public int Initial_time;

    public StaticCoinManager coinManager;

    public TextMeshProUGUI timerText;
    public static Timer staticTimer;

    void Start()
    {
        if (coinManager == null)
        {
            Debug.LogError("CoinManager is not assigned in StaticPlayerManager.");
        }

        // timer = GetComponent<Timer>();
        // if (timer == null)
        // {
        //     Debug.LogError("Timer component not found on StaticPlayerManager");
        // }

        // timer.InitializeTimer(Initial_time);
        // timer.setTMP();
            GameObject timerObject = GameObject.Find("PlayerTest for static"); // Find the TimerObject
        if (timerObject == null)
        {
            Debug.LogError("TimerObject not found in the scene.");
            return;
        }

        staticTimer = timerObject.GetComponent<Timer>();
        if (staticTimer == null)
        {
            Debug.LogError("Timer component not found on TimerObject.");
            return;
        }
        staticTimer.InitializeTimer(Initial_time);
        staticTimer.setTMP();
    }

    void Update()
    {
        if (staticTimer.TimeLeft <= 0)
        {
            // Game over logic...
        }
    }

    // Public methods to add and subtract time
    public void AddTime(int timeToAdd)
    {
        Debug.Log("add time called inside static player manager");
        staticTimer.AddTime(timeToAdd);
    }

    public void SubtractTime(int timeToSubtract)
    {
        Debug.Log("subtract time called inside static player manager");
        staticTimer.SubtractTime(timeToSubtract);
    }
}
