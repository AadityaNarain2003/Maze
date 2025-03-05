using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticFireballManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject fireballContainer; // New: The parent container GameObject
    public float speed = 2f;
    public float respawnDelay = 5f;
    public int fire_subtract; // Time to subtract when player hits a fireball
    public StaticPlayerManager playerManager; // Reference to the player manager

    private Dictionary<GameObject, Tuple<Transform, Transform>> activeFireballs = new Dictionary<GameObject, Tuple<Transform, Transform>>();
    private Dictionary<GameObject, bool> fireballDirection = new Dictionary<GameObject, bool>();

    void Start()
    {
        if (fireballContainer == null)
        {
            Debug.LogError("fireballContainer is not assigned in StaticFireballManager.");
            return; // Exit early if container is not assigned.
        }
        InitializeFireballs();
    }

    void InitializeFireballs()
    {
        foreach (Transform childTransform in fireballContainer.GetComponentsInChildren<Transform>())
        {
            Debug.Log("Checking child: " + childTransform.name);
            //Ignore the parent
            if (childTransform == fireballContainer.transform) continue;
            Transform startTransform = null;
            Transform targetTransform = null;
            foreach (Transform grandChildTransform in childTransform.GetComponentsInChildren<Transform>())
            {
                Debug.Log("Checking grandchild: " + grandChildTransform.name);
                if (grandChildTransform.name.Contains("s")) // Changed to "s"
                {
                    startTransform = grandChildTransform;
                }
                else if (grandChildTransform.name.Contains("t")) // Changed to "t"
                {
                    targetTransform = grandChildTransform;
                }
            }

            if (startTransform == null || targetTransform == null)
            {
                Debug.LogError("Start or target transform not found for " + childTransform.name);
                continue;
            }

            GameObject fireball = Instantiate(fireballPrefab, startTransform.position, Quaternion.identity);
            FireballCollider fireballCollider = fireball.AddComponent<FireballCollider>();
            fireballCollider.fireballManager = this;
            activeFireballs.Add(fireball, new Tuple<Transform, Transform>(startTransform, targetTransform));
            fireballDirection.Add(fireball, true);
        }
    }

    void Update()
    {
        foreach (KeyValuePair<GameObject, Tuple<Transform, Transform>> data in activeFireballs)
        {
            GameObject currentFireball = data.Key;
            Tuple<Transform, Transform> currentData = data.Value;
            bool currentDirection = fireballDirection[currentFireball];

            Transform destination = currentDirection ? currentData.Item2 : currentData.Item1;

            // Move towards the current destination
            currentFireball.transform.position = Vector3.MoveTowards(currentFireball.transform.position, destination.position, speed * Time.deltaTime);

            // Switch direction when reaching the destination
            if (Vector3.Distance(currentFireball.transform.position, destination.position) < 0.1f)
            {
                fireballDirection[currentFireball] = !currentDirection; // Toggle direction
            }
        }
    }

    public void HandleFireballCollision(GameObject fireball)
    {
        if (activeFireballs.ContainsKey(fireball))
        {
            Debug.Log("Fireball collision detected inside staticFireManager!");
            Tuple<Transform, Transform> data = activeFireballs[fireball];
            activeFireballs.Remove(fireball);
            fireballDirection.Remove(fireball);
            Destroy(fireball);
            playerManager.SubtractTime(fire_subtract); // Notify the player to reduce the time.
            StartCoroutine(RespawnFireball(data));
        }
    }

    private IEnumerator RespawnFireball(Tuple<Transform, Transform> data)
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject newFireball = Instantiate(fireballPrefab, data.Item1.position, Quaternion.identity);
        FireballCollider fireballCollider = newFireball.AddComponent<FireballCollider>();
        fireballCollider.fireballManager = this;
        activeFireballs.Add(newFireball, data);
        fireballDirection.Add(newFireball, true);
    }
}
