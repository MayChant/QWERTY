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
    void DisplayInMinutes(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        buttonText.text = "QUACK (" + string.Format("{0:0}:{1:00}", minutes, seconds) + ")";
    }
    void Update()
    {
        if (gameManager.gameState != GameManager.GameState.Quack)
        {

            buttonText.text = "QUACK";
        }
        else
        {
            DisplayInMinutes(timer.time);
        }

    }
}
