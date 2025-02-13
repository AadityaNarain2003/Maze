using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticFireBalls : MonoBehaviour
{
    public GameObject fireballPrefab; // Prefab of the fireball
    public List<Transform> startPositions; // Fixed start positions for fireballs
    public List<Transform> targetPositions; // Fixed target positions for fireballs
    public float speed = 0f; // Speed of the fireball
    private float fireballGenerationDelay = 2f; // Delay for regenerating a fireball

    private List<GameObject> activeFireballs = new List<GameObject>(); // List of active fireballs

    private void Start()
    {
        GenerateFireballs();
        // MoveFireballs();
    }

    private void Update()
    {
        MoveFireballs();
    }

    // Generate fireballs at start positions
    private void GenerateFireballs()
    {
        for (int i = 0; i < startPositions.Count; i++)
        {
            GenerateFireball(startPositions[i], targetPositions[i],i);
        }
    }

    // Generate a fireball at a specific position
    private void GenerateFireball(Transform spawnTransform, Transform targetTransform, int i)
    {
        GameObject newFireball = Instantiate(fireballPrefab, spawnTransform.position, Quaternion.identity);
        newFireball.GetComponent<Fireball>().Initialize(spawnTransform.position,targetTransform.position, speed, i);
        activeFireballs.Add(newFireball);
    }

    // Move all active fireballs
    private void MoveFireballs()
    {
        foreach (var fireball in activeFireballs)
        {
            fireball.GetComponent<Fireball>().Move();
        }
    }

    // Handle fireball interaction and regenerate it after a delay
    public void HandleFireballInteraction(GameObject fireball)
    {
        Vector3 position = fireball.transform.position;
        activeFireballs.Remove(fireball);
        Destroy(fireball);
        int index = fireball.GetComponent<Fireball>().index;
        StartCoroutine(GenerateFireballAfterDelay(position,index));
    }

    // Coroutine to regenerate a fireball at the start position after the specified delay
    private IEnumerator GenerateFireballAfterDelay(Vector3 position, int index)
    {
        yield return new WaitForSeconds(fireballGenerationDelay);
        // int index = startPositions.FindIndex(t => t.position == position);
        GenerateFireball(startPositions[index], targetPositions[index],index);
        // MoveFireballs();
    }
}