using UnityEngine;

public class StaticFireBalls : MonoBehaviour
{
    public Vector3 targetPosition; // Set location B
    public float speed=0f;      // Speed of the fireball

    private void Update()
    {
        // Move the fireball towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy the fireball when it reaches the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
