using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextDIsplay : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public Text buttonText;
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        buttonText = gameObject.transform.Find("Text").GetComponent<Text>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if(gameStateManager.gameState != GameStateManager.GameState.Quack)
        {
            
            buttonText.text = "Submit";
        }
        else
        {
            buttonText.text = "Submit (" + Math.Floor(gameStateManager.timer.time).ToString() + ")";
        }
        
    }
}
