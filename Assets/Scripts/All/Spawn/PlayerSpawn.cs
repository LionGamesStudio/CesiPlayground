using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public bool isActivated = false;


    // Update is called once per frame
    void Start()
    {
        if (isActivated)
        {
            SpawnPlayer();
        }
        
    }

    public void SpawnPlayer()
    {
        // Search for the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Set the player position to the spawn position
            player.transform.position = transform.position;
        }
    }

}
