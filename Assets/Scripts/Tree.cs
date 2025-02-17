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
    public void createChildren(MazeNode parent,int distanceLeft,int distanceRight)
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
        Debug.Log(val);
        parent.createLeftWall(wall,midpointLeft,parent.IncomingDirection,GetDistanceLeft-1,coin,val<=0.5,fire,val>0.5);
        parent.createRightWall(wall,midpointRight,parent.IncomingDirection,GetDistanceRight-1,coin,val>0.5,fire,val<=0.5);

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
        if (mazeNode.Parent != null)
        {
            mazeNode.Parent.nodeobject.SetActive(true);
            mazeNode.Parent.nodewallobject.SetActive(true);
            mazeNode.Parent.leftWall.SetActive(true);
            mazeNode.Parent.rightWall.SetActive(true);
            if(mazeNode.Parent.coin!=null)
            {
                mazeNode.Parent.coin.SetActive(true);
            }
            activeNode.Add(mazeNode.Parent);
        }   

    // Add the left child node, if it exists, and set it active
        if (mazeNode.Left != null)
        {
            mazeNode.Left.nodeobject.SetActive(true);
            mazeNode.Left.nodewallobject.SetActive(true);
            activeNode.Add(mazeNode.Left);
        }

    // Add the right child node, if it exists, and set it active
        if (mazeNode.Right != null)
        {
            mazeNode.Right.nodeobject.SetActive(true);
            mazeNode.Right.nodewallobject.SetActive(true);
            activeNode.Add(mazeNode.Right);
        }
    }

}