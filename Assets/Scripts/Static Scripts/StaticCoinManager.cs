using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoinManager : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab of the coin
    public GameObject spawnPointsContainer; // New: The parent container GameObject
    public float coinGenerationDelay = 2f; // Delay for generating a new coin
    public StaticPlayerManager playerManager;// Reference to the player manager

    private Dictionary<GameObject, Transform> activeCoins = new Dictionary<GameObject, Transform>(); // List of active coins, and the position

    void Start()
    {
        if (spawnPointsContainer == null)
        {
            Debug.LogError("spawnPointsContainer is not assigned in StaticCoinManager.");
            return; // Exit early if container is not assigned.
        }
        // Generate coins at ALL spawn positions
        foreach (Transform childTransform in spawnPointsContainer.GetComponentsInChildren<Transform>())
        {
            //Ignore the parent
            if (childTransform == spawnPointsContainer.transform) continue;
            GenerateCoin(childTransform);
        }
    }

    // Generate a coin at a specific position
    public void GenerateCoin(Transform spawnTransform) // Now takes a Transform
    {
        GameObject coin = Instantiate(coinPrefab, spawnTransform.position, spawnTransform.rotation); //Use transform properties
        CoinCollider coinCollider = coin.GetComponent<CoinCollider>();
        coinCollider.coinManager = this;
        activeCoins.Add(coin, spawnTransform); //Save the initial transform
    }

    // Handle coin collection and generate a new one after a delay
    public void HandleCoinCollision(GameObject coin)
    {
        // Remove the coin from the list and destroy it
        if (activeCoins.ContainsKey(coin))
        {
            Debug.Log("Coin collision detected inside staticCoinManager!");
            Transform position = activeCoins[coin]; //Get the initial transform
            activeCoins.Remove(coin);
            Destroy(coin);
            playerManager.AddTime(playerManager.coin_add); // Notify the player to add the coin time.
            // Start coroutine to generate a new coin at the same position after a delay
            StartCoroutine(GenerateCoinAfterDelay(position)); //Pass the transform
        }
    }

    // Coroutine to generate a new coin at a the same position after the specified delay
    private IEnumerator GenerateCoinAfterDelay(Transform position) // now recieves a transform
    {
        yield return new WaitForSeconds(coinGenerationDelay);
        GenerateCoin(position); //Pass the transform
    }
}
