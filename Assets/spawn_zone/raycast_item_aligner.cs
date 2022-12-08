using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class raycast_item_aligner : MonoBehaviour
{
    public float raycast_distance = 100f;
    public GameObject[] toSpawn;
    public float distance_sol = 0.5f;
    public float Overlap_Textbox_Size = 1f;
    public LayerMask Spawn_Layer_Mask;
    private static int nb_spawn = 0;
    void Pick(Vector3 PositionToSpawn, Quaternion RotationToSpawn){ //creation de la liste des items à créer à un emplacement aléatoire d'une zone
        int randomIndex = Random.Range(0,toSpawn.Length);
        GameObject clone = Instantiate(toSpawn[randomIndex],PositionToSpawn,RotationToSpawn);
    }
    void Position_Raycast() {
        RaycastHit Ray;
        if (Physics.Raycast(transform.position, Vector3.down, out Ray, raycast_distance))
        {
            float _y = Ray.point.y + (distance_sol * Random.Range(0.2f, 3f));
            Vector3 position_spawn = new Vector3(Ray.point.x, _y, Ray.point.z);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, Ray.normal);

            Vector3 Overlap_Scale = new Vector3(1, Overlap_Textbox_Size, Overlap_Textbox_Size);
            Collider[] Collider_Inside_Overlap_Box = new Collider[1];
            int nombre_collision = Physics.OverlapBoxNonAlloc(position_spawn, Overlap_Scale, Collider_Inside_Overlap_Box, rotation, Spawn_Layer_Mask);
            if (nombre_collision==0)
            {
                nb_spawn++;
                Pick(position_spawn, rotation);
            }
        }
    }
    // Start is called before the first frame update
    void Start() {
        Position_Raycast();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int getnNbItemsSpawn()
    {
        return nb_spawn;
    }
}
