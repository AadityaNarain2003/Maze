using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public MazeNode currentNode;
    public MazeNode firstNode;
    public List<MazeNode> activeNode;

    public GameObject nodePrefab;

    public GameObject nodewallPrefab;

    public GameObject wall;

    public GameObject fire;
    public GameObject coin; 

    public Tree(GameObject prefab,GameObject wallprefab,GameObject coinprefab,GameObject fire,GameObject wall, Vector3 initialpos, Vector3 initialDirection)
    {
        nodePrefab=prefab;
        nodewallPrefab=wallprefab;
        coin=coinprefab;
        this.wall=wall;
        this.fire=fire;
        firstNode=new MazeNode(initialpos,0,initialDirection,nodePrefab,nodewallPrefab);
        currentNode=firstNode;
        
        activeNode = new List<MazeNode>();
        activeNode.Add(firstNode);
    }
    public void createChildrenRoot(MazeNode parent,float distanceLeft,float distanceRight,int value)
    {
        if(value>1)
        {
            return;
        }
        Vector3 directionleft=rotateLeft(parent.IncomingDirection);
        Vector3 PositionLeft=parent.Position+directionleft*distanceLeft;
        MazeNode left=new MazeNode(PositionLeft,parent.Level+1,directionleft,nodePrefab,nodewallPrefab,parent);
        parent.Left=left;

        Vector3 directionRight=rotateRight(parent.IncomingDirection);
        Vector3 PositionRight=parent.Position+directionRight*distanceRight;
        MazeNode right=new MazeNode(PositionRight,parent.Level+1,directionRight,nodePrefab,nodewallPrefab,parent);
        parent.Right=right;

        activeNode.Add(left);
        activeNode.Add(right);

        //now I need to create the paths from the parent to the children
        Vector3 midpointLeft=GetMidpoint(parent,left);
        Vector3 midpointRight=GetMidpoint(parent,right);
        float GetDistanceLeft=GetDistance(parent,left);
        float GetDistanceRight=GetDistance(parent,right);
        float val=Random.value;
        // Debug.Log(val);
        parent.createLeftWall(wall,midpointLeft,parent.IncomingDirection,GetDistanceLeft-1,coin,val<=0.5,fire,val>0.5);
        parent.createRightWall(wall,midpointRight,parent.IncomingDirection,GetDistanceRight-1,coin,val>0.5,fire,val<=0.5);

        float[] possibleDistances = new float[] {2.5F}; //,2.3f,2.5f, 2.0f

        float randomLeftDistance = possibleDistances[Random.Range(0, possibleDistances.Length)];
        float randomRightDistance = possibleDistances[Random.Range(0, possibleDistances.Length)];

        createChildrenRoot(left, randomLeftDistance, randomRightDistance,value+1);

        randomLeftDistance = possibleDistances[Random.Range(0, possibleDistances.Length)];
        randomRightDistance = possibleDistances[Random.Range(0, possibleDistances.Length)];

        createChildrenRoot(right, randomLeftDistance, randomRightDistance,value+1);

    }
    public void createChildren(MazeNode parent,float distanceLeft,float distanceRight)
    {
        Vector3 directionleft=rotateLeft(parent.IncomingDirection);
        Vector3 PositionLeft=parent.Position+directionleft*distanceLeft;
        MazeNode left=new MazeNode(PositionLeft,parent.Level+1,directionleft,nodePrefab,nodewallPrefab,parent);
        parent.Left=left;

        Vector3 directionRight=rotateRight(parent.IncomingDirection);
        Vector3 PositionRight=parent.Position+directionRight*distanceRight;
        MazeNode right=new MazeNode(PositionRight,parent.Level+1,directionRight,nodePrefab,nodewallPrefab,parent);
        parent.Right=right;

        activeNode.Add(left);
        activeNode.Add(right);

        //now I need to create the paths from the parent to the children
        Vector3 midpointLeft=GetMidpoint(parent,left);
        Vector3 midpointRight=GetMidpoint(parent,right);
        float GetDistanceLeft=GetDistance(parent,left);
        float GetDistanceRight=GetDistance(parent,right);
        float val=Random.value;
        // Debug.Log(val);
        parent.createLeftWall(wall,midpointLeft,parent.IncomingDirection,GetDistanceLeft-1,coin,val<=0.5,fire,val>0.5);
        parent.createRightWall(wall,midpointRight,parent.IncomingDirection,GetDistanceRight-1,coin,val>0.5,fire,val<=0.5);
        Debug.Log("Child Created");

    }

    public Vector3 rotateLeft(Vector3 direction)
    {
        return new Vector3(direction.z, direction.y, -direction.x);
    }
    public Vector3 rotateRight(Vector3 direction)
    {
        return new Vector3(-direction.z, direction.y, direction.x);
    }

    
    public Vector3 GetMidpoint(MazeNode nodeA, MazeNode nodeB)
    {
        // Ensure neither node is null before accessing their positions
        if (nodeA == null || nodeB == null)
        {
            Debug.LogError("One or both nodes are null!");
            return Vector3.zero;
        }

        // Calculate and return the midpoint
        return (nodeA.Position + nodeB.Position) / 2.0f;
    }
    public float GetDistance(MazeNode nodeA, MazeNode nodeB)
    {
        if (nodeA == null || nodeB == null)
        {
            Debug.LogError("One or both nodes are null!");
            return -1f; // Return a negative value to indicate an error
        }

        // Calculate and return the distance between the two positions
        return Vector3.Distance(nodeA.Position, nodeB.Position);
    }

    public void fixActiveMap(MazeNode mazeNode)
    {
    // Set all current nodes in activeNode to inactive
        foreach (MazeNode node in activeNode)
        {
            if (node.nodeobject != null)
            {
                node.nodeobject.SetActive(false);
                node.nodewallobject.SetActive(false);
                if(node.leftWall!=null)
                {
                    node.leftWall.SetActive(false);
                }
                if(node.rightWall!=null)
                {
                    node.rightWall.SetActive(false);    
                }
                if(node.fireball!=null)
                {
                    node.fireball.SetActive(false);
                }
                if(node.leftWall!=null)
                {
                    node.leftWall.SetActive(false);
                }
                if(node.rightWall!=null)
                {
                    node.rightWall.SetActive(false);
                }
                if(node.initialnodeWall!=null)
                {
                    node.initialnodeWall.SetActive(false);
                }  
                if(node.coin!=null)
                {
                    node.coin.SetActive(false);
                }
            }
        }

    // Clear the activeNode list
        activeNode.Clear();

    // Add the mazeNode itself and set it active
        mazeNode.nodeobject.SetActive(true);
        mazeNode.nodewallobject.SetActive(true);
        if(mazeNode.leftWall!=null)
        {
            mazeNode.leftWall.SetActive(true);
        }
        if(mazeNode.rightWall!=null)
        {
            mazeNode.rightWall.SetActive(true);
        }
        if(mazeNode.initialnodeWall!=null)
        {
            mazeNode.initialnodeWall.SetActive(true);
        }
        if(mazeNode.coin!=null)
        {
            mazeNode.coin.SetActive(true);
        }
        if(mazeNode.fireball!=null)
        {
            mazeNode.fireball.SetActive(true);  
        }
        activeNode.Add(mazeNode);
        

    // Add the parent node, if it exists, and set it active
        // Add the parent node, if it exists, and set it active
if (mazeNode.Parent != null)
{
    mazeNode.Parent.nodeobject.SetActive(true);
    if (mazeNode.Parent.nodewallobject != null) 
    {
        mazeNode.Parent.nodewallobject.SetActive(true);
    }
    if (mazeNode.Parent.leftWall != null) 
    {
        mazeNode.Parent.leftWall.SetActive(true);
    }
    if (mazeNode.Parent.rightWall != null) 
    {
        mazeNode.Parent.rightWall.SetActive(true);
    }
    if (mazeNode.Parent.coin != null) 
    {
        mazeNode.Parent.coin.SetActive(true);
    }
    if (mazeNode.Parent.fireball != null) 
    {
        mazeNode.Parent.fireball.SetActive(true);
    }
    activeNode.Add(mazeNode.Parent);

    if (mazeNode.Parent.Parent != null)
    {
        mazeNode.Parent.Parent.nodeobject.SetActive(true);
        if (mazeNode.Parent.Parent.nodewallobject != null) 
        {
            mazeNode.Parent.Parent.nodewallobject.SetActive(true);
        }
        if (mazeNode.Parent.Parent.leftWall != null) 
        {
            mazeNode.Parent.Parent.leftWall.SetActive(true);
        }
        if (mazeNode.Parent.Parent.rightWall != null) 
        {
            mazeNode.Parent.Parent.rightWall.SetActive(true);
        }
        if (mazeNode.Parent.Parent.coin != null) 
        {
            mazeNode.Parent.Parent.coin.SetActive(true);
        }
        if (mazeNode.Parent.Parent.fireball != null) 
        {
            mazeNode.Parent.Parent.fireball.SetActive(true);
        }
        activeNode.Add(mazeNode.Parent.Parent);
    }
}


    // Add the left child node, if it exists, and set it active
        if (mazeNode.Left != null)
        {
            mazeNode.Left.nodeobject.SetActive(true);
            mazeNode.Left.nodewallobject.SetActive(true);
            if(mazeNode.Left.leftWall != null)
            {
                mazeNode.Left.leftWall.SetActive(true);
            }
            if(mazeNode.Left.rightWall != null)
            {
                mazeNode.Left.rightWall.SetActive(true);
            }
            if(mazeNode.Left.coin!=null)
            {
                mazeNode.Left.coin.SetActive(true);
            }
            if(mazeNode.Left.fireball!=null)
            {
                mazeNode.Left.fireball.SetActive(true);
            }
            activeNode.Add(mazeNode.Left);

            if(mazeNode.Left.Left!=null)
            {
                mazeNode.Left.Left.nodeobject.SetActive(true);
                mazeNode.Left.Left.nodewallobject.SetActive(true);
                activeNode.Add(mazeNode.Left.Left);
            }
            if(mazeNode.Left.Right!=null)
            {
                mazeNode.Left.Right.nodeobject.SetActive(true);
                mazeNode.Left.Right.nodewallobject.SetActive(true);
                activeNode.Add(mazeNode.Left.Right);
            }
        }

    // Add the right child node, if it exists, and set it active
        if (mazeNode.Right != null)
        {
            mazeNode.Right.nodeobject.SetActive(true);
            mazeNode.Right.nodewallobject.SetActive(true);
            if(mazeNode.Right.leftWall!=null)
            {
                mazeNode.Right.leftWall.SetActive(true);
            }
            if(mazeNode.Right.rightWall!=null)
            {
                mazeNode.Right.rightWall.SetActive(true);
            }
            if(mazeNode.Right.coin!=null)
            {
                mazeNode.Right.coin.SetActive(true);
            }
            if(mazeNode.Right.fireball!=null)
            {
                mazeNode.Right.fireball.SetActive(true);
            }
            activeNode.Add(mazeNode.Right);

            if(mazeNode.Right.Left!=null)
            {
                mazeNode.Right.Left.nodeobject.SetActive(true);
                mazeNode.Right.Left.nodewallobject.SetActive(true);
                activeNode.Add(mazeNode.Right.Left);
            }
            if(mazeNode.Right.Right!=null)
            {
                mazeNode.Right.Right.nodeobject.SetActive(true);
                mazeNode.Right.Right.nodewallobject.SetActive(true);
                activeNode.Add(mazeNode.Right.Right);
            }
        }
    }

}