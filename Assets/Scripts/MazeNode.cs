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

    public GameObject leftWall, rightWall;

    public bool active { get; private set; }

    public GameObject nodeobject;

    public GameObject initialnodeWall;

    public GameObject nodewallobject;

    public GameObject coin;

    public GameObject fireball;
    public bool isCoinPresent;

    private GameObject coinAnimationHandlerObject;

    public MazeNode(Vector3 position, int level, Vector3 incomingDirection, GameObject node, GameObject nodewall, MazeNode parent = null)
    {
        Position = position;
        Level = level;
        IncomingDirection = incomingDirection;
        Parent = parent;
        active = true;
        Left = null;
        Right = null;
        leftWall = null;
        rightWall = null;
        initialnodeWall = null;
        coin = null;
        isCoinPresent = false;
        fireball=null;

        nodeobject = GameObject.Instantiate(node, position, Quaternion.identity);

        Vector3 wallPosition = position + IncomingDirection * 0.5f;
        Quaternion wallRotation = Quaternion.LookRotation(IncomingDirection);

        // Create the wall object
        nodewallobject = GameObject.Instantiate(nodewall, wallPosition, wallRotation);

        MazeNodeComponent nodeComponent = nodeobject.AddComponent<MazeNodeComponent>();
        nodeComponent.Initialize(this);
    }

    public void createLeftWall(GameObject wall, Vector3 position, Vector3 direction, float scale, GameObject coin, bool isCoin,GameObject fire, bool isFire)
    {
        Quaternion directionLook = Quaternion.LookRotation(direction);
        leftWall = GameObject.Instantiate(wall, position, directionLook);
        Vector3 wallScale = new Vector3(scale, 1, 1);
        leftWall.transform.localScale = wallScale;

        if (isCoin)
        {
            position.y = 0.5F;
            isCoinPresent = true;
            this.coin = GameObject.Instantiate(coin, position, Quaternion.identity);

            // Create a GameObject for the CoinAnimationHandler and add the handler to it
            coinAnimationHandlerObject = new GameObject("CoinAnimationHandler");
            CoinAnimationHandler coinAnimationHandler = coinAnimationHandlerObject.AddComponent<CoinAnimationHandler>();

            // Start the coroutine to animate the coin after it has been instantiated
            coinAnimationHandler.StartCoroutine(coinAnimationHandler.StartCoinAnimation(this.coin));
        }
        if(isFire && this.Level!=0)
        {
            Vector3 pos=this.Left.Position;
            Vector3 currpos=this.Position;
            pos.y=0.5F;
            currpos.y=0.5F;

           this.fireball = GameObject.Instantiate(fire, pos, Quaternion.identity);

            // Ensure the fireball has a Rigidbody
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("here");
                // Calculate direction from 'pos' to 'currpos'
                Vector3 dir = (currpos - pos).normalized;

                // Set the velocity to 2 units per second in the calculated direction
                rb.velocity = dir * 2f;
            }
        }   
    }

    public void createRightWall(GameObject wall, Vector3 position, Vector3 direction, float scale, GameObject coin, bool isCoin,GameObject fire, bool isFire)
    {
        Quaternion directionLook = Quaternion.LookRotation(direction);
        rightWall = GameObject.Instantiate(wall, position, directionLook);
        Vector3 wallScale = new Vector3(scale, 1, 1);
        rightWall.transform.localScale = wallScale;

        if (isCoin)
        {
            isCoinPresent = true;
            position.y = 0.5F;
            this.coin = GameObject.Instantiate(coin, position, Quaternion.identity);

            // Create a GameObject for the CoinAnimationHandler and add the handler to it
            coinAnimationHandlerObject = new GameObject("CoinAnimationHandler");
            CoinAnimationHandler coinAnimationHandler = coinAnimationHandlerObject.AddComponent<CoinAnimationHandler>();

            // Start the coroutine to animate the coin after it has been instantiated
            coinAnimationHandler.StartCoroutine(coinAnimationHandler.StartCoinAnimation(this.coin));
        }
        if(isFire && this.Level!=0)
        {
            Vector3 pos=this.Right.Position;
            Vector3 currpos=this.Position;
            pos.y=0.5F;
            currpos.y=0.5F;

           this.fireball = GameObject.Instantiate(fire, pos, Quaternion.identity);

            // Ensure the fireball has a Rigidbody
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("here");
                // Calculate direction from 'pos' to 'currpos'
                Vector3 dir = (currpos - pos).normalized;

                // Set the velocity to 2 units per second in the calculated direction
                rb.velocity = dir * 0.75f;
            }

        }
    }
}
