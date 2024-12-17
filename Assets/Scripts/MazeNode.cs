using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode 
{
    public Vector3 Position { get; private set; } // The center of the node in world space
    public int Level { get; private set; } // Depth of the node
    public Vector3 IncomingDirection { get; private set; } // Direction player came from
    public MazeNode Parent { get; private set; } // Parent node for backtracking
    public MazeNode Left { get; set; } // Left child node
    public MazeNode Right { get; set; } // Right child node

    public GameObject leftWall,rightWall;

    public bool active { get; private set; }

    public GameObject nodeobject;

    public GameObject initialnodeWall;

    public GameObject nodewallobject;
    public MazeNode(Vector3 position, int level, Vector3 incomingDirection,GameObject node,GameObject nodewall, MazeNode parent = null)
    {
        //Debug.Log("Node Created");
        Position = position;
        Level = level;
        IncomingDirection = incomingDirection;
        Parent = parent;
        active=true;
        Left=null;
        Right=null;
        leftWall=null;
        rightWall=null;
        initialnodeWall=null;
        nodeobject = GameObject.Instantiate(node, position, Quaternion.identity);
        //Debug.Log(position);
        //Debug.Log(incomingDirection);

        Vector3 wallPosition = position + IncomingDirection * 0.5f;
        Quaternion wallRotation = Quaternion.LookRotation(IncomingDirection);

        // Create the wall object
        nodewallobject = GameObject.Instantiate(nodewall, wallPosition, wallRotation);


        MazeNodeComponent nodeComponent = nodeobject.AddComponent<MazeNodeComponent>();
        nodeComponent.Initialize(this);
    }
    public void createLeftWall(GameObject wall, Vector3 position, Vector3 direction, float scale)
    {
        Quaternion directionLook= Quaternion.LookRotation(direction);
        leftWall=GameObject.Instantiate(wall, position,directionLook);
        Vector3 wallScale = new Vector3(scale,1,1);
        leftWall.transform.localScale = wallScale;
    }
    public void createRightWall(GameObject wall, Vector3 position, Vector3 direction, float scale)
    {
        Quaternion directionLook= Quaternion.LookRotation(direction);
        rightWall=GameObject.Instantiate(wall, position, directionLook);
        Vector3 wallScale = new Vector3(scale,1,1);
        rightWall.transform.localScale = wallScale;
    }
    
}
