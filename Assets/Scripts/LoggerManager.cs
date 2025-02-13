using UnityEngine;
using System.Collections;

public class LoggerManager : MonoBehaviour
{
    [SerializeField]
    private Transform objectToLog; // Drag the target object into this field in the Inspector

    private void Start()
    {
        // Initialize the logger with the object to track
        TransformLogger.Initialize(objectToLog);
        Debug.Log("Logger Manager Called");
    }
}
