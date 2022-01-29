using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public enum GameState{

menu,
inGame,
gameOver,

credits

}

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager sharedInstance; 


    public GameState currentGameState  = GameState.menu;
    private Whale controller;

    public GameObject pauseMenuUI;

    
    
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
        controller = GameObject.FindWithTag("Player").GetComponent<Whale>();
        BackToMenu();
    }

    void Update()
    {
        
       if(Input.GetKeyDown(KeyCode.S) && currentGameState != GameState.inGame){
           StartGame();
           isPaused=false;
       } 
       if(Input.GetButton("Cancel") && currentGameState == GameState.inGame){

           if(isPaused){
               Resume();
           } else{
               Pause();
           }

       } 
        
    }


    public void StartGame(){ //Inicia el videojuego
        isPaused = false;
        SetGameState(GameState.inGame);
        

    }

    public void GameOver(){ //Fin del juego por muerte
      SetGameState(GameState.gameOver);
      
    
    }

    public void mainScene(){
        SetGameState(GameState.menu);
        
    }

    public void BackToMenu(){ // Volver al menú principal
      SetGameState(GameState.menu);
    }

    public void creditMenu(){
        SetGameState(GameState.credits);
    }

    private void SetGameState (GameState newGameState){
        if(newGameState == GameState.menu){
            MenuManager.sharedInstanceMenu.ShowMainMenu();
            MenuManager.sharedInstanceMenu.HideHud();
            MenuManager.sharedInstanceMenu.Hidecredit();
            MenuManager.sharedInstanceMenu.HideGameOver();

        } else if (newGameState == GameState.inGame){
            controller.StartGame();
            MenuManager.sharedInstanceMenu.HideMainMenu();
            MenuManager.sharedInstanceMenu.ShowHud();
            MenuManager.sharedInstanceMenu.Hidecredit();
            MenuManager.sharedInstanceMenu.HideGameOver();

        } else if (newGameState == GameState.gameOver){
            MenuManager.sharedInstanceMenu.HideMainMenu();
            MenuManager.sharedInstanceMenu.HideHud();
            MenuManager.sharedInstanceMenu.Hidecredit();
            MenuManager.sharedInstanceMenu.ShowGameOver();

        } else if (newGameState == GameState.credits){
            MenuManager.sharedInstanceMenu.HideMainMenu();
            MenuManager.sharedInstanceMenu.HideHud();
            MenuManager.sharedInstanceMenu.ShowCredit();
            MenuManager.sharedInstanceMenu.HideGameOver();
        
        }
        this.currentGameState = newGameState;
    }

    public void Resume (){

        pauseMenuUI.SetActive(false);
        Time.timeScale =1;
        isPaused= false;
    }

    void Pause (){
        
        pauseMenuUI.SetActive(true);
        Time.timeScale =0;
        isPaused= true;
    }

        
}
