using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private StaticCoinManager coinManager;
    private StaticFireballManager fireballManager;
    // public Transform cameraTransform; // Reference to the headset
    // private Vector3 previousCameraPosition;

    void Start()
    {
        // if (cameraTransform == null)
        // {
        //     cameraTransform = Camera.main.transform; // Automatically find the VR headset
        // }
        // previousCameraPosition = cameraTransform.position;

        Debug.Log("PlayerCollisionHandler Start");
        // Find the Manager GameObject in the scene.
        GameObject managerObject = GameObject.Find("Manager");

        if (managerObject == null)
        {
            Debug.LogError("Manager GameObject not found in the scene.");
            return;
        }

        // Get the StaticCoinManager and StaticFireballManager components from the Manager GameObject.
        coinManager = managerObject.GetComponent<StaticCoinManager>();
        fireballManager = managerObject.GetComponent<StaticFireballManager>();

        if (coinManager == null)
        {
            Debug.LogError("StaticCoinManager not found on the Manager GameObject.");
        }

        if (fireballManager == null)
        {
            Debug.LogError("StaticFireballManager not found on the Manager GameObject.");
        }
    }

    // void Update()
    // {
    //     Vector3 headsetMovement = cameraTransform.position - previousCameraPosition;
    //     headsetMovement.y = 0;  // Prevent extra vertical movement

    //     // Apply the movement to PlayerTest
    //     transform.position += headsetMovement;

    //     // Update the previous position
    //     previousCameraPosition = cameraTransform.position;
    // }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerCollisionHandler OnTriggerEnter");
        Debug.Log("Player collided with: " + other.gameObject.name);
        Debug.Log("Player collided with layer: " + other.gameObject.layer);
        // Check if the collided object is a coin.
        if (other.gameObject.layer == LayerMask.NameToLayer("coin"))
        {
            Debug.Log("Player collided with a coin before coinmanager!");
            if (coinManager != null)
            {
                Debug.Log("Player collided with a coin!");
                // We need to pass the coin game object so that coin manager can identify which coin it is.
                coinManager.HandleCoinCollision(other.gameObject);
            }
        }
        //Check if the collided object is a fireball
        else if (other.gameObject.layer == LayerMask.NameToLayer("fireball"))
        {   
            Debug.Log("Player collided with a fireball before fireballmanager!");
             if (fireballManager != null)
            {
                Debug.Log("Player collided with a fireball!");
                //We need to pass the fireball game object so that fireball manager can identify which fireball it is.
                fireballManager.HandleFireballCollision(other.gameObject);
            }
        }
    }
}
