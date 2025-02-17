using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab; // Drag your Fireball prefab here
    public Transform spawnPoint;      // Location A (Start Position)
    public Transform targetTransform; // Location B (Target)
    public float spawnInterval = 2f;  // Interval between spawns
    public float speed;               // Speed of fireball

    private void Start()
    {
        //InvokeRepeating(nameof(SpawnFireball), 0f, spawnInterval);
        SpawnFireball();
    }

    private void SpawnFireball()
    {
        // Instantiate the fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);

        // Set its target position
        StaticFireBalls fireballScript = fireball.GetComponent<StaticFireBalls>();
        if (fireballScript != null && targetTransform != null)
        {
            fireballScript.startTransform = spawnPoint; 
            fireballScript.targetTransform = targetTransform; // Assigning Transform instead of Vector3
            fireballScript.speed = speed;
        }
    }
}
