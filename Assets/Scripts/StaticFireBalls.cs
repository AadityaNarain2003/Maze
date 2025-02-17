using UnityEngine;

public class StaticFireBalls : MonoBehaviour
{
    public Transform targetTransform; 
    public Transform startTransform;
    public float speed = 2f;

    private bool movingToTarget = true;

    private void Update()
    {
        if (targetTransform != null && startTransform != null)
        {
            Transform destination = movingToTarget ? targetTransform : startTransform;

            // Move towards the current destination
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);

            // Switch direction when reaching the destination
            if (Vector3.Distance(transform.position, destination.position) < 0.1f)
            {
                movingToTarget = !movingToTarget;  // Toggle direction
            }
        }
    }
}
