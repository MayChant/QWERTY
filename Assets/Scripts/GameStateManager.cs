using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public Text dialogue;
    public Text userInput;
    public Button nextQuackButton;
    public Button submitButton;
    public Timer timer;
    public GameManager gameManager;
    public enum GameState{
        Intro,
        Quack,
        FeedBack,
        Ending,
        Endless,
    }
    public GameState gameState; 
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    private void Start() {
        dialogue = GameObject.Find("Queck").GetComponent<Text>();
        userInput = GameObject.Find("Input").GetComponent<Text>();
        nextQuackButton = GameObject.Find("NextQuackButton").GetComponent<Button>();
        submitButton = GameObject.Find("SubmitButton").GetComponent<Button>();
        timer = gameObject.GetComponent( typeof(Timer) ) as Timer;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameState = GameState.Quack;
    }
    void Update()
    {
        switch (gameState)
        {
            case GameState.FeedBack:
                dialogue.text = gameManager.feedback;
                nextQuackButton.gameObject.SetActive(true);
                submitButton.gameObject.SetActive(false);
                
                break;
            case GameState.Quack:
                dialogue.text = gameManager.currentQuack;
                nextQuackButton.gameObject.SetActive(false);
                submitButton.gameObject.SetActive(true);
                
                break;
            default:
                dialogue.text = "this is a default dialogue.";
                nextQuackButton.gameObject.SetActive(false);
                break;
        }
        
    }

    public void StartNextQuack(){
        gameState = GameState.Quack;
        userInput.text = "";
        timer.time = 180;
        gameManager.ToNextQuack();
    }
    public void SubmitCurrentQuack(){
        int errorCount = (userInput.GetComponent( typeof(KeyboardMixer) ) as KeyboardMixer).Submit();
        gameManager.ProcessSubmission(errorCount);
        gameState = GameState.FeedBack;
        
    }
}
