using UnityEngine;

public class StaticFireBalls : MonoBehaviour
{
    public Transform targetTransform; // Target as a Transform
    public float speed = 0f;          // Speed of the fireball

    private void Update()
    {
        if (targetTransform != null)
        {
            // Move the fireball towards the target's current position
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

            // Destroy the fireball when it reaches the target
            if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
