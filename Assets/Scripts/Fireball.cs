using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int index;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 currPosition;
    private float speed;

    public void Initialize(Vector3 startPosition, Vector3 endPosition, float speed, int index)
    {
        this.index = index;
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.currPosition = endPosition;
        this.speed = speed;
    }

    private void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currPosition) < 0.1f)
        {
            Debug.Log("Hello it reached the target");
            SwapPosition();
            // Move();
        }
    }
    public void SwapPosition()
    {
        if (currPosition == endPosition)
        {
            currPosition = startPosition;
        }
        else
        {
            currPosition = endPosition;
        }
    }

}