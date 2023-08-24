using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiGameOver : MonoBehaviour
{
    public TMP_Text inf;
    public MyGameManager gm;

    public void Restart(){
        // Debug.Log("restart");
        SceneManager.LoadScene(0);
        gm.Init();
        Time.timeScale = 1;
    }

    public void Update(){
        UpdateText();
    }

    private void UpdateText(){
        //Debug.Log(String.Format("You {0}!", MyGameManager.Board.GetInstance().win ? "win" : "lose"));
        string text;
        if(MyGameManager.Board.GetInstance().win){
            text = "You win!";
        }else{
            text = "You lose!";
        }
        inf.text = text;
    } 
}
