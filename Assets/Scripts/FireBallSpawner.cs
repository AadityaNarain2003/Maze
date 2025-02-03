using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab; // Drag your Fireball prefab here
    public Transform spawnPoint;      // Location A
    public Vector3 targetPosition;    // Location B
    public float spawnInterval = 2f;  // Interval between spawns

    public float speed;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFireball), 0f, spawnInterval);
    }

    private void SpawnFireball()
    {
        // Instantiate the fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("FireBall Spawner");
        // Set its target position
        StaticFireBalls fireballScript = fireball.GetComponent<StaticFireBalls>();
        if (fireballScript != null)
        {
            Debug.Log("Fireball Script Not Found Error");
            fireballScript.targetPosition = targetPosition;
            fireballScript.speed = speed;
        }
    }
}
