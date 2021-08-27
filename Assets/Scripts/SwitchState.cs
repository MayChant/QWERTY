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
     private string[] intro;
    public Button startButton;
    public Text dialogue;
    public Text playerDialogue;
    public string currentIntro;
    public int quackIndex = 0;
    private void Awake() {
        intro = introCollection.collection.ToArray();
        currentIntro = quacks[quackIndex];
       
    }
    void Start()
    {
        gameManager = GameManager.instance;
        if(gameManager.gameState == GameManager.GameState.Intro)
        {
            
            textWriter.AddTextToWrite(dialogue.text, currentIntro, 0.02f);
        }
    }
    public void NextIntroDialogue()
    {
        quackIndex += 1;
        currentIntro = quacks[quackIndex];
        if(gameManager.gameState == GameManager.GameState.Intro)
        {
            textWriter.AddTextToWrite(dialogue.text, currentIntro, 0.02f);
        }
        if((quackIndex + 1) > intro.Length)
        {
            gameManager.gameState = GameManager.GameState.Quack;
        }
    }

    //a function that display next dialog
    //if is the end, change game state
    // Update is called once per frame
}
