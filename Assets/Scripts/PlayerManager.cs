using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    void Start()
    {
        player=new Player(gameObject.transform.position,-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
