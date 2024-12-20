using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 Position { get; private set; }  // The player's position in world space
    public int Level { get; private set; }         // The player's current level in the maze

    public float time_traversed;

    // Constructor to initialize player position and level
    public Player(Vector3 position, int level)
    {
        Position = position;
        Level = level;
        time_traversed=0;
    }
    public void Update()    
    {
        time_traversed+= Time.deltaTime;
    }
}