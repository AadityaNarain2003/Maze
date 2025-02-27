using UnityEngine;

public class FireballCollider : MonoBehaviour
{
    public StaticFireballManager fireballManager; // Reference to the manager

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the player has touched the fireball
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Notify the manager of the collision
            if (fireballManager != null)
            {
                fireballManager.HandleFireballCollision(gameObject);
            }
        }
    }
}
