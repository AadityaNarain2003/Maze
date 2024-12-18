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
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initialPosition= new Vector3(0,1.5f,0);
        Vector3 initialDirection = new Vector3(0, 0,1);
        //Debug.Log("Hello");
        gameTree=new Tree(nodeprefab,nodewallprefab,coinprefab,wall,initialPosition,initialDirection);

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
        //Debug.Log($"MazeGenerator received node interaction. Position: {mazeNode.Position}, Level: {mazeNode.Level}");
        gameTree.currentNode = mazeNode;
        gameTree.fixActiveMap(mazeNode);
        if(mazeNode.Left==null && mazeNode.Right==null)
        {
            float[] possibleDistances = new float[] {  1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

            // Randomly select distance values for the left and right child creation
            float randomLeftDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];
            float randomRightDistance = possibleDistances[Random.Range(1, possibleDistances.Length)];

            // Create children with the random distances
            gameTree.createChildren(mazeNode, (int)randomLeftDistance,(int) randomRightDistance);
        }
    }

    void update(){}
}
