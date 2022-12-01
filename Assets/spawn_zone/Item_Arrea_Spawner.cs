using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item_Arrea_Spawner : MonoBehaviour
{
    public GameObject itemsToSpread;
    public float X_item = 0;
    public float Y_item = 1;
    public float Z_item = 3.32f;
    public int Number_item = 42;
    void Spread_Items()
    {       //spawn items in random location
        Vector3 Rand_position= new Vector3(Random.Range(-X_item,X_item),Random.Range(-Y_item,Y_item),Random.Range(-Z_item,Z_item))+transform.position;
        GameObject clone = Instantiate(itemsToSpread,Rand_position,Quaternion.Euler(0f,180f,0f));
    }
    // Start is called before the first frame update
    void Start()
    {       //spawn de number_item cibles
        int nb_item_spawn = 0;
        int index = 0;
        while (nb_item_spawn < Number_item)
        {
            Spread_Items();
            nb_item_spawn = itemsToSpread.GetComponent<raycast_item_aligner>().getnNbItemsSpawn();
            index++;
            if (index >= 100)
            {
                break;
            }
        }
    }
}
