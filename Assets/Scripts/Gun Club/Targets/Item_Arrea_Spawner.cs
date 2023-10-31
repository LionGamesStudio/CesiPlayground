using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item_Arrea_Spawner : MonoBehaviour
{
    public GameObject itemsToSpread;
    public DataItemSpread levelData;
    private int nbCibleAlive = 0;


    void Spread_Items()
    {       //spawn items in random location
        Vector3 Rand_position= new Vector3(Random.Range(-levelData.X_item,levelData.X_item),Random.Range(-levelData.Y_item,levelData.Y_item),Random.Range(-levelData.Z_item,levelData.Z_item))+transform.position;
        GameObject clone = Instantiate(itemsToSpread,Rand_position,Quaternion.Euler(0f,0f,0f));
    }
    // Start is called before the first frame update
    void Start()
    {
        nbCibleAlive = levelData.Number_item;
       //spawn de number_item cibles
        for (int i = 0; i < levelData.Number_item; i++)
        {
            Spread_Items();
        }
        
    }
    
    //coroutine pour baisser le nombre d'objet présent
    public IEnumerator LowerNbItems()
    {
        nbCibleAlive--;
        yield return null;
    }

    private void Update()
    {
        
        if (nbCibleAlive <= 0)
        {
            nbCibleAlive = levelData.Number_item;
            for (int i = 0; i < levelData.Number_item; i++)
            {
                Spread_Items();
            }
        }
    }
}
