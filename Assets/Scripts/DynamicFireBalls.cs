using UnityEngine;

public class DynamicFireBalls : MonoBehaviour
{
    public Transform targetTransform; 
    public Transform startTransform;
    public float speed = 1.0F;
    public float patrolDistance = 2.0f; // Distance to move left/right from center

    private bool movingToTarget = true;
    private MazeNode mazeNode;

    public void instantizeBallLeft()
    {
        if (mazeNode == null)
        {
            Debug.LogWarning("Cannot instantiate fireball - invalid node");
            return;
        }

        Vector3 centerPos = mazeNode.Position;
        centerPos.y = 0.5F;
        Vector3 leftPos = centerPos + Vector3.left * patrolDistance;
        Vector3 rightPos = centerPos + Vector3.right * patrolDistance;

        CreateFireballPath(leftPos, rightPos);
    }

    public void instantizeBallRight()
    {
        if (mazeNode == null)
        {
            Debug.LogWarning("Cannot instantiate fireball - invalid node");
            return;
        }

        Vector3 centerPos = mazeNode.Position;
        centerPos.y = 0.5F;
        Vector3 leftPos = centerPos + Vector3.left * patrolDistance;
        Vector3 rightPos = centerPos + Vector3.right * patrolDistance;

        CreateFireballPath(leftPos, rightPos);
    }

    private void CreateFireballPath(Vector3 startPos, Vector3 endPos)
    {
        GameObject startObj = new GameObject("StartPoint");
        startObj.transform.position = startPos;
        GameObject endObj = new GameObject("EndPoint");
        endObj.transform.position = endPos;
            
        startTransform = startObj.transform; 
        targetTransform = endObj.transform;
    }

    public void setNode(MazeNode mazeNode)
    {
        this.mazeNode = mazeNode;
    }

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