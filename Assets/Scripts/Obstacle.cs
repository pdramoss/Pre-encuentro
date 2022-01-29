using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public bool can_harm = false;
    public int damage = 20;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (can_harm){
        if (other.gameObject.CompareTag("Player"))
            {
            other.gameObject.GetComponent<Whale>().Harm(damage);
            }
        if (other.gameObject.CompareTag("Baby"))
            {
            other.gameObject.GetComponent<Baby>().Harm(damage*2);
            }
        }
    }
}
