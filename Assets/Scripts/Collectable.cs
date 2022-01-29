using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType{
    food
}
public class Collectable : MonoBehaviour
{

    public CollectableType type = CollectableType.food;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;

    bool hasBeenCollected = false;

    public int value = 1;


    private void Awake (){
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }


    void Show (){
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    void Hide (){
        
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    void Collect (){
           Hide();
           hasBeenCollected = true;

      switch(this.type){
          case CollectableType.food:
          GameObject Whale = GameObject.Find("whale");
          Whale.GetComponent<Whale>().CollectHealth(this.value);
          break;
      }
        
    }
   void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            Collect();
        }
    }
}
