using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public enum GameState{

menu,
inGame,
gameOver

}

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager sharedInstance; 


    public GameState currentGameState  = GameState.menu;
    private Whale controller;
    
    [SerializeField]  public bool isPaused = false;

    private void Awake(){

        if(sharedInstance == null){
            sharedInstance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else{
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        controller = GameObject.FindWithTag("Whale").GetComponent<Whale>();
        StartGame();
    }

    void Update()
    {
        
       if(Input.GetKeyDown(KeyCode.S)){
           StartGame();
           isPaused=false;
       } 
       if(Input.GetButton("Cancel")){
           BackToMenu();
           isPaused=true;
       } 
        
    }


    public void StartGame(){ //Inicia el videojuego
        isPaused = false;
        SetGameState(GameState.inGame);
    }

    public void GameOver(){ //Fin del juego por muerte
      SetGameState(GameState.gameOver);   
    
    }

    public void BackToMenu(){ // Volver al menú principal
      SetGameState(GameState.menu);
    }

    private void SetGameState (GameState newGameState){
        if(newGameState == GameState.menu){
        } else if (newGameState == GameState.inGame){
        } else if (newGameState == GameState.gameOver){
        }

        this.currentGameState = newGameState;
    }

    
    
}
