using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticPlayerManager : MonoBehaviour
{
    private Player player;
    private Timer timer; // Keep it private

    public int coin_add;
    public int Initial_time;

    public StaticCoinManager coinManager;

    public TextMeshProUGUI timerText;

    void Start()
    {
        if (coinManager == null)
        {
            Debug.LogError("CoinManager is not assigned in StaticPlayerManager.");
        }

        timer = GetComponent<Timer>();
        if (timer == null)
        {
            Debug.LogError("Timer component not found on StaticPlayerManager");
        }

        timer.InitializeTimer(Initial_time);
        timer.setTMP();
    }

    void Update()
    {
        if (timer.TimeLeft <= 0)
        {
            // Game over logic...
        }
    }

    // Public methods to add and subtract time
    public void AddTime(int timeToAdd)
    {
        timer.AddTime(timeToAdd);
    }

    public void SubtractTime(int timeToSubtract)
    {
        timer.SubtractTime(timeToSubtract);
    }
}
