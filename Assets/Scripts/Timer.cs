using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public KeyboardMixer keyboardMixer;
    private GameManager gameManager;
    public int countTime;
    public int initialCountTime;
    void Start(){
        gameManager = GameManager.instance;
        countTime = initialCountTime;
        time = countTime;
    }
    private void OnEnable() {
        if(gameManager != null)
        {
            if(gameManager.gameState != GameManager.GameState.Endless)
            {
                countTime -= 5;
            }
            time = countTime;
        }
        
    }
    void Update()
    {
        if(time > 0 && gameManager.gameState == GameManager.GameState.Quack)
        {
            time -= Time.deltaTime;
        }
        else
        {
            keyboardMixer.ChooseSubmit();
        }
    }
}
