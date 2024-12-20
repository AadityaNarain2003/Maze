using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public Timer timer;

    public int coin_add;

    public int fire_subtract;

    public int Initial_time;
    void Start()
    {
        player=new Player(gameObject.transform.position,-1);
        timer = gameObject.AddComponent<Timer>();
        timer.InitializeTimer(Initial_time); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Time Left: {timer.TimeLeft}");

        // Check if time has run out
        if (timer.TimeLeft <= 0)
        {
            Debug.Log("Time's up!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // For coins
        if (other.gameObject.layer == LayerMask.NameToLayer("coin"))
        {
            Destroy(other.gameObject);
            timer.AddTime(coin_add); // Add time to the timer
        }

        // For fireballs
        if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
        {
            Destroy(other.gameObject);
            timer.SubtractTime(fire_subtract); // Subtract time from the timer
        }
    }
}
