using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoinManager : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab of the coin
    public List<Vector3> spawnPositions; // Fixed spawn positions for coins
    public float coinGenerationDelay = 2f; // Delay for generating a new coin

    private List<GameObject> activeCoins = new List<GameObject>(); // List of active coins
    public int initialCoinCount = 5; // Number of coins to spawn at the start

    void Start()
    {
        // Generate initial set of coins
        for (int i = 0; i < initialCoinCount; i++)
        {
            if (i < spawnPositions.Count) // Ensure not to exceed the defined spawn positions
            {
                GenerateCoin(spawnPositions[i]);
            }
        }
    }

    // Generate a coin at a specific position
    public void GenerateCoin(Vector3 spawnPosition)
    {
        Debug.Log("SPW");
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        activeCoins.Add(newCoin);
    }

    // Handle coin collection and generate a new one after a delay
    public void HandleCoinCollection(GameObject coin)
    {
        // Remove the coin from the list and destroy it
        activeCoins.Remove(coin);
        Destroy(coin);

        // Start coroutine to generate a new coin at a random fixed position after a delay
        StartCoroutine(GenerateCoinAfterDelay());
    }

    // Coroutine to generate a new coin at a random fixed position after the specified delay
    private IEnumerator GenerateCoinAfterDelay()
    {
        yield return new WaitForSeconds(coinGenerationDelay);

        // Pick a random position from the predefined list of spawn positions
        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GenerateCoin(spawnPosition);
    }
}
