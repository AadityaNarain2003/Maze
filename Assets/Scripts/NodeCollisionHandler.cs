using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class NodeCollisionHandler : MonoBehaviour
{
    public static event Action<MazeNode> OnNodeInteracted;
    private bool hasLogged = false;
    private bool hasTriggered=false;
    private void OnTriggerEnter(Collider other)
    {
        //this is for Node and player
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && hasTriggered==false)
        {
            hasTriggered=true;
            Debug.Log("Player collided with Node: " + gameObject.name);

            // Get the MazeNodeComponent from this GameObject
            MazeNodeComponent nodeComponent = GetComponent<MazeNodeComponent>();
            if (nodeComponent != null && nodeComponent.Node != null)
            {
                MazeNode mazeNode = nodeComponent.Node;

                // Invoke the event with the MazeNode data
                OnNodeInteracted?.Invoke(mazeNode);

                // Debug to verify
                //Debug.Log($"MazeNode Position: {mazeNode.Position}, Level: {mazeNode.Level}");
                if(hasLogged==false)
                {
                    hasLogged=true;
                    //TransformLogger.UpdateLoggerFile(mazeNode);
                }
            }
        }
        
    }
     private void OnTriggerExit(Collider other)
    {
        hasTriggered=false;
        hasLogged=false;
    }

}
