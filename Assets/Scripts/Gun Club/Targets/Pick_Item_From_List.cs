using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Utilisé dans le script Assets/Scripts/Gun%20Club/spawn_targets/Spawnzone prefab
public class Pick_Item_From_List : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;

    void Pick(){
        int randomIndex = Random.Range(0,itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex],transform.position,Quaternion.identity);
     }

    // Start is called before the first frame update
    void Start()
    {
        Pick();        
    }
}
