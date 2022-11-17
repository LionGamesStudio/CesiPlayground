using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Arrea_Spawner : MonoBehaviour
{
    public GameObject itemsToSpread;
    public float X_item = 0;
    public float Y_item = 1;
    public float Z_item = 3.32f;
    public int Number_item = 42;
    void Spread_Items()
    {
        Vector3 Rand_position= new Vector3(Random.Range(-X_item,X_item),Random.Range(-Y_item,Y_item),Random.Range(-Z_item,Z_item))+transform.position;
        GameObject clone = Instantiate(itemsToSpread,Rand_position,Quaternion.Euler(0f,180f,0f));
    }
    // Start is called before the first frame update
    void Start()
    {   
        for (int i=0; i<Number_item;i++){
            Spread_Items();
        }
    }
}
