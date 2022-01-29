using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVidew : MonoBehaviour
{
     private Whale controller;
    // Start is called before the first frame update
    void Start()
    {
          controller = GameObject.Find("whale").GetComponent<Whale>();
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
              float dist = controller.GetDistance();
        }
        
    }
}
