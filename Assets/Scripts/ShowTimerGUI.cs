// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;
// // using UnityEngine.UI;

// // public class ShowTimerGUI : MonoBehaviour
// // {
    

// //     public Text timerText;
// //     private Timer timer;

// //     private StaticPlayerManager playerManager;

// //     void Awake()
// //     {
// //         playerManager = gameObject.GetComponent<StaticPlayerManager>();
// //     }
// //     void Start()
// //     {
// //         // playerManager = gameObject.GetComponent<StaticPlayerManager>();
// //         timer = playerManager.timer;
// //     }

// //     // Update is called once per frame
// //     void Update()
// //     {

// //     }
// //     // Start is called before the first frame update


// //     // Update is called once per frame    void OnGUI()
// //     void OnGUI()
// //     {
// //         GUIStyle style = new GUIStyle();
// //         style.fontSize = 24;
// //         style.normal.textColor = Color.white;

// //         GUI.Label(new Rect(10, 10, 200, 50), "Time Left: " + timer.TimeLeft.ToString("F2"), style);
// //     }


// // }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ShowTimerGUI : MonoBehaviour
// {
//     public Text timerText;
//     private Timer _timer;

//     private StaticPlayerManager playerManager;

//     void Awake()
//     {
//         playerManager = gameObject.GetComponent<StaticPlayerManager>();
//         if (playerManager == null)
//         {
//             Debug.LogError("StaticPlayerManager component not found on the GameObject.");
//         }
//     }

//     void Start()
//     {
//         if (playerManager != null)
//         {
//             _timer = playerManager.timer;
//             if (_timer == null)
//             {
//                 Debug.LogError("Timer is not initialized in StaticPlayerManager.");
//             }
//         }
//     }

//     void Update()
//     {
//         // Update logic if needed
//     }

//     void OnGUI()
//     {
//         if (_timer != null)
//         {
//             GUIStyle style = new GUIStyle();
//             style.fontSize = 24;
//             style.normal.textColor = Color.white;

//             GUI.Label(new Rect(10, 10, 200, 50), "Time Left: " + _timer.TimeLeft.ToString("F2"), style);
//         }
//         else
//         {
//             Debug.LogError("Timer is not set.");
//         }
//     }
// }
