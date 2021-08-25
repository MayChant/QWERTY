using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public KeyboardMixer keyboardMixer;
    private GameManager gameManager;
    void Start(){
        gameManager = GameManager.instance;
    }
    private void OnEnable() {
        time = 180;
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
