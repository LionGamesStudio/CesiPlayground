using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScore : MonoBehaviour
{
    public int life = 200;
    public int value = 5;
    public GameObject balle;
    public GameObject score;

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(this, 0f);
            score.GetComponent<Scoring>().score += value;
            score.GetComponent<Scoring>().UpdateText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == balle)
        {
            TakeDamage(collision.gameObject.GetComponent<Damage>().damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
