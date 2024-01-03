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
        // Search player in all scene
        GameObject playerXR = GameObject.Instantiate(Resources.Load("VR XR/Prefab/PlayerXR") as GameObject);
        if(playerXR != null)
        {
            // Set player position to the spawn position
            playerXR.transform.position = transform.position;
        }
    }

}
