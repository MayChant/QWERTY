using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextDisplay : MonoBehaviour
{
    private GameManager gameManager;
    public Text buttonText;
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }
    // Update is called once per frame
    
    void Update()
    {
        if(gameManager.gameState != GameManager.GameState.Quack)
        {
            
            buttonText.text = "QUACK";
        }
        else
        {
            buttonText.text = "QUACK (" + Math.Floor(timer.time).ToString() + ")";
        }
        
    }
}
