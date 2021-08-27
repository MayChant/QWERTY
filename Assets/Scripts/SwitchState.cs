using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public TextWriter textWriter;
    public QuackCollection introCollection;
    public QuackCollection endingCollection;
     private string[] intro;
    public Button startButton;
    public Text dialogue;
    public Text playerDialogue;
    public string currentIntro;
    public int quackCount = 0;
    public int introIndex = 0;
    private void Awake() {
        intro = introCollection.collection;
        currentIntro = intro[introIndex];
       
    }
    void Start()
    {
        gameManager = GameManager.instance;
        if(gameManager.gameState == GameManager.GameState.Intro)
        {

            textWriter.AddTextToWrite(dialogue, currentIntro, 0.02f);
        }
    }
    public void NextDialogue()
    {
        switch(gameManager.gameState){
            case GameManager.GameState.Intro:
                if((introIndex + 1) >= intro.Length)
                {
                    gameManager.gameState = GameManager.GameState.Quack;
                    gameManager.ToNextQuack();
                    startButton.gameObject.SetActive(false);
                }
                else
                {
                    introIndex += 1;
                    currentIntro = intro[introIndex];
                    if(gameManager.gameState == GameManager.GameState.Intro)
                    {
                        textWriter.AddTextToWrite(dialogue, currentIntro, 0.02f);
                    }       
                
                }
                break;
        }
        
        
    }


    //a function that display next dialog
    //if is the end, change game state
    // Update is called once per frame
}
