using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private Timer timer;

    public int coin_add;

    public int fire_subtract;

    public int Initial_time;

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
            FindObjectOfType<DamageEffect>().TriggerDamageEffect(Color.green);
            Debug.Log("Player collided with Coin"); 
            Destroy(other.gameObject);
            timer.AddTime(coin_add); // Add time to the timer
        }

        // For fireballs
        if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
        {
            Debug.Log("Player collided with Fireball"); 
            //Destroy(other.gameObject);
            FindObjectOfType<DamageEffect>().TriggerDamageEffect(Color.red);
            timer.SubtractTime(fire_subtract); // Subtract time from the timer
        }
    }
}
