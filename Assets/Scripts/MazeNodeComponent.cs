using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNodeComponent : MonoBehaviour
{
    public MazeNode Node { get; private set; }

    public void Initialize(MazeNode mazeNode)
    {
        Node = mazeNode;
    }
}

