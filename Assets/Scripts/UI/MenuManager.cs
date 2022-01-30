using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   public static MenuManager sharedInstanceMenu;
    public Canvas menuCanvas;

    public Canvas HUD;

    public Canvas Credit;

    public Canvas GameOver;
      
    private void Awake (){

      if(sharedInstanceMenu == null){
          sharedInstanceMenu = this;
      }

    }

    public void ShowMainMenu (){
        menuCanvas.enabled = true;
    }

    public void ShowHud(){
        HUD.enabled = true;
    }

    public void ShowCredit(){
        Credit.enabled = true;
    }

    public void ShowGameOver(){
        GameOver.enabled = true;
    }

    public void HideMainMenu (){
        menuCanvas.enabled = false;
    }

    public void HideHud(){
        HUD.enabled = false;
    }

    public void Hidecredit(){
        Credit.enabled = false;
    }

    public void HideGameOver(){
        GameOver.enabled = false;
    }

    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

            #else
               Application.Quit();

        #endif
    }
    
    public void PlayClickSound(){
        AudioManager.PlaySound(AudioManager.Sound.sfx_ui_click, false, 0.4f);
    }


    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
