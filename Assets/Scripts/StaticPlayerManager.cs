// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class StaticPlayerManager : MonoBehaviour
// {
//     // Start is called before the first frame update
//     private Player player;
//     public Timer timer;

//     public int coin_add;

//     public int fire_subtract;

//     public int Initial_time;

//     public StaticCoinManager coinManager;
//     void Start()
//     {
//         player=new Player(gameObject.transform.position,-1);
//         timer = gameObject.AddComponent<Timer>();
//         timer.InitializeTimer(Initial_time); 
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //Debug.Log($"Time Left: {timer.TimeLeft}");
        
//         // Check if time has run out
//         if (timer.TimeLeft <= 0)
//         {
//            Debug.Log("Time's up!");
//         }
//     }

//     void OnGUI()
//     {
//         GUIStyle style = new GUIStyle();
//         style.fontSize = 24;
//         style.normal.textColor = Color.white;

//         GUI.Label(new Rect(10, 10, 200, 50), "Time Left: " + timer.TimeLeft.ToString("F2"), style);
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         // For coins
//         if (other.gameObject.layer == LayerMask.NameToLayer("coin"))
//         {
//             coinManager.HandleCoinCollection(other.gameObject);
//             timer.AddTime(coin_add); // Add time to the timer
//             Debug.Log($"Coin Collected and Time Left: {timer.TimeLeft}");
//         }

//         // For fireballs
//         if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
//         {
//             Destroy(other.gameObject);
//             timer.SubtractTime(fire_subtract); // Subtract time from the timer
//             Debug.Log($"Ball Touched and Time Left: {timer.TimeLeft}");
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlayerManager : MonoBehaviour
{
    // private Player player;
    public Timer timer;

    public int coin_add;
    public int fire_subtract;
    public int Initial_time;

    public StaticCoinManager coinManager;
    public StaticFireBalls fireBalls;

    void Start()
    {
        // player = new Player(gameObject.transform.position, -1);
        Debug.Log(gameObject);
        timer = gameObject.AddComponent<Timer>();
        fireBalls = gameObject.GetComponent<StaticFireBalls>();
        timer.InitializeTimer(Initial_time);
    }

    void Update()
    {
        if (timer.TimeLeft <= 0)
        {
            Debug.Log("Time's up!");
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // Create a semi-transparent box
        Color originalColor = GUI.color;
        GUI.color = new Color(0, 0, 0, 0.5f); // Set the color to black with 50% transparency
        GUI.Box(new Rect(10, 10, 220, 70), GUIContent.none); // Draw the box
        GUI.color = originalColor; // Reset the color to original

        // Draw the timer text inside the box
        GUI.Label(new Rect(20, 20, 200, 50), "Time Left: " + Mathf.FloorToInt(timer.TimeLeft).ToString(), style);

        if (timer.TimeLeft <= 0)
        {
            GUIStyle endStyle = new GUIStyle();
            endStyle.fontSize = 48;
            endStyle.normal.textColor = Color.red;
            endStyle.alignment = TextAnchor.MiddleCenter;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none);
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 50), "Time is Over", endStyle);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("coin"))
        {
            coinManager.HandleCoinCollection(other.gameObject);
            timer.AddTime(coin_add);
            Debug.Log($"Coin Collected and Time Left: {timer.TimeLeft}");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
        {
            fireBalls.HandleFireballInteraction(other.gameObject);
            // Destroy(other.gameObject);
            timer.SubtractTime(fire_subtract);
            Debug.Log($"Ball Touched and Time Left: {timer.TimeLeft}");
        }
    }
}