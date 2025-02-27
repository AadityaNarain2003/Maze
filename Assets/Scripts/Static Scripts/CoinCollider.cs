using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public StaticCoinManager coinManager; // Reference to the manager

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the player has touched the coin
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Notify the manager of the collision
            if (coinManager != null)
            {
                coinManager.HandleCoinCollision(gameObject);
            }
        }
    }
}
