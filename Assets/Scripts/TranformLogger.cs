using UnityEngine;
using System.IO;

public static class TransformLogger
{
    // File name and path
    private static string filePath;

    // Transform to log
    private static Transform targetTransform;

    // Initialize the logger (call this at game start)
    public static void Initialize(Transform target)
    {
        // Set the target Transform to log
        targetTransform = target;

        // Set file path in persistent data directory
        filePath = Path.Combine(Application.persistentDataPath, "TransformLogTest-3.txt");

        // Create or clear the file
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("Timestamp, Position (x, y, z), Rotation (x, y, z), Scale (x, y, z)");
        }

        Debug.Log($"Log file created at: {filePath}");
    }

    // Log transform data
    public static void UpdateLoggerFile(MazeNode mazeNode)
    {
        if (targetTransform != null)
        {
            // Get transform data
            Vector3 position = mazeNode.Position;
            //Vector3 rotation = mazeNode.eulerAngles;
            //Vector3 scale = mazeNode.localScale;

            // Prepare the log entry
            string logEntry = $"{Time.time}, {position.x:F2}, {position.y:F2}, {position.z:F2} ";// +
                             // $"{rotation.x:F2}, {rotation.y:F2}, {rotation.z:F2}, " +
                              //$"{scale.x:F2}, {scale.y:F2}, {scale.z:F2}";

            // Append to the file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(logEntry);
            }
            Debug.Log("Written");
        }
        else
        {
            Debug.LogWarning("Target Transform is not set. Unable to log data.");
        }
    }
}
