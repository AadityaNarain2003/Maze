using UnityEngine;

public class DynamicFireBalls : MonoBehaviour
{
    public Transform targetTransform; 
    public Transform startTransform;
    public float speed = 2f;

    private bool movingToTarget = true;

    private MazeNode mazeNode;

    public void instantizeBallLeft()
    {
            Vector3 pos=mazeNode.Left.Position;
            Vector3 currpos=mazeNode.Position;
            Vector3 opppos= mazeNode.Right.Position;
            pos.y=0.5F;
            currpos.y=0.5F;
            GameObject startObj = new GameObject("StartPoint");
            startObj.transform.position = pos;
            GameObject endObj = new GameObject("EndPoint");
            endObj.transform.position = opppos;
            
            startTransform = startObj.transform; 
            targetTransform = endObj.transform; 
            speed = 3;       
    }

    public void instantizeBallRight()
    {
            Vector3 pos=mazeNode.Right.Position;
            Vector3 currpos=mazeNode.Position;
            Vector3 opppos= mazeNode.Left.Position;
            pos.y=0.5F;
            currpos.y=0.5F;
            GameObject startObj = new GameObject("StartPoint");
            startObj.transform.position = pos;
            GameObject endObj = new GameObject("EndPoint");
            endObj.transform.position = opppos;
            
            startTransform = startObj.transform; 
            targetTransform = endObj.transform; 
            speed = 3;       
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
