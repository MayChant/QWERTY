using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public GameStateManager gameStateManager;
    void Start(){
        gameStateManager = (gameObject.GetComponent( typeof(GameStateManager) ) as GameStateManager);
    }
    private void OnEnable() {
        time = 180;
    }
    void Update()
    {
        if(time > 0 && gameStateManager.gameState == GameStateManager.GameState.Quack)
        {
            time -= Time.deltaTime;
        }
        else
        {
            gameStateManager.SubmitCurrentQuack();
        }
    }
}
