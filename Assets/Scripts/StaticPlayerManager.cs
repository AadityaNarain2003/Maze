using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticPlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private Timer timer;

    public int coin_add;

    public int fire_subtract;

    public int Initial_time;

    public StaticCoinManager coinManager;

    public TextMeshProUGUI timerText;
    void Start()
    {
        //player=new Player(gameObject.transform.position,-1);
        timer = gameObject.AddComponent<Timer>();
        timer.InitializeTimer(Initial_time); 
        timer.setTMP();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Time Left: {timer.TimeLeft}");

        // Check if time has run out
        if (timer.TimeLeft <= 0)
        {
           // Debug.Log("Time's up!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // For coins
        if (other.gameObject.layer == LayerMask.NameToLayer("coin"))
        {
            coinManager.HandleCoinCollection(other.gameObject);
            timer.AddTime(coin_add); // Add time to the timer
        }

        // For fireballs
        if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
        {
            //Destroy(other.gameObject);
            timer.SubtractTime(fire_subtract); // Subtract time from the timer
        }
    }
}
