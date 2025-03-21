using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject nodeprefab;
    public GameObject nodewallprefab;
    
    public GameObject wall;

    public GameObject coinprefab;
    public Tree gameTree;

    public GameObject fireprefab;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initialPosition= new Vector3(0,1.5f,0);
        Vector3 initialDirection = new Vector3(0, 0,1);
        Debug.Log("Hello");
        if (wall == null) Debug.LogError("Wall prefab is null at MazeGenerator!");
        if (coinprefab == null) Debug.LogError("Coin prefab is null at MazeGenerator!");
        if (fireprefab == null) Debug.LogError("Fire prefab is null at MazeGenerator!");
        gameTree=new Tree(nodeprefab,nodewallprefab,coinprefab,fireprefab,wall,initialPosition,initialDirection);

        Vector3 wallPosition = gameTree.currentNode.Position - gameTree.currentNode.IncomingDirection * 0.5f;
        Quaternion wallRotation = Quaternion.LookRotation(gameTree.currentNode.IncomingDirection);

        // Create the wall object
        gameTree.currentNode.initialnodeWall = GameObject.Instantiate(nodewallprefab, wallPosition, wallRotation);

        NodeCollisionHandler.OnNodeInteracted += HandleNodeInteraction;

        gameTree.activeNode.Add(gameTree.currentNode);
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        NodeCollisionHandler.OnNodeInteracted -= HandleNodeInteraction;
    }

    // Method to handle node interactions
    private void HandleNodeInteraction(MazeNode mazeNode)
    {
        Debug.Log($"MazeGenerator received node interaction. Position: {mazeNode.Position}, Level: {mazeNode.Level}");
        gameTree.currentNode = mazeNode;
        gameTree.fixActiveMap(mazeNode);
        float[] possibleDistances = new float[] { 1.5f,2.3f };
         

        // Randomly select distance values for the left and right child creation
        float randomLeftDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];
        float randomRightDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];

        if(mazeNode.Left==null && mazeNode.Right==null)
        {
            
            // Create children with the random distances
            if(mazeNode.Level==0)
            {
                Debug.Log("IN IF Case");
                gameTree.createChildrenRoot(mazeNode, (int)randomLeftDistance,(int) randomRightDistance,0);
            }
            
        }
        else
        {
            if( mazeNode.Left.Left==null && mazeNode.Right.Left==null && mazeNode.Left.Right==null && mazeNode.Right.Right==null )
            {
                if(mazeNode.Level!=0)
                {
                    Debug.Log("IN ELSE CASE");

                    randomLeftDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];
                    randomRightDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];

                    gameTree.createChildren(mazeNode.Left, randomLeftDistance, randomRightDistance);

                    randomLeftDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];
                    randomRightDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];

                    gameTree.createChildren(mazeNode.Right, randomLeftDistance, randomRightDistance);
                }
            }
        }
    }
}
