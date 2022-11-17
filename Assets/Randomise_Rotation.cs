using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomise_Rotation : MonoBehaviour
{
    void Randomise_rotation()
    {
        transform.rotation = Quaternion.Euler(0,180,Random.Range(0,360));
    }
    // Start is called before the first frame update
    void Start()
    {
        Randomise_rotation();
    }
}
