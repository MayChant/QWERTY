using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Hongjian_ver : MonoBehaviour
{
    //public static GameManager instance;
    public QuackCollection quackCollection;
    private string[] quacks;
    private int quackIndex;
    public string currentQuack;
    public string feedback;
    public int lives;
    public int streak;
    public bool isGivingFeedback;
    public Text dialogue;
    public Text userInput;
    public Button nextQuackButton;
    public Timer timer;
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
        // if (instance == null)
        // {
        //     instance = this;
        // } else if (instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        // DontDestroyOnLoad(gameObject);
        // quacks = quackCollection.collection.OrderBy(x => Random.value).ToArray();
        // currentQuack = quacks[quackIndex];
        // lives = 10;
        // isGivingFeedback = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.FeedBack:
                dialogue.text = feedback;
                nextQuackButton.gameObject.SetActive(true);
                
                break;
            case GameState.Quack:
                dialogue.text = currentQuack;
                nextQuackButton.gameObject.SetActive(false);
                
                break;
            default:
                dialogue.text = "this is a default dialogue.";
                nextQuackButton.gameObject.SetActive(false);
                break;
        }
        
    }

    public void ToNextQuack()
    {
        quackIndex++;
        if(quackIndex >= quacks.Length ){
            quackIndex = 0;
        }
        currentQuack = quacks[quackIndex];
        gameState = GameState.Quack;
        timer.time = 180;
    }

    public void ProcessSubmission(int errorCount)
    {
        if (errorCount == 0)
        {
            streak++;
            if (streak == 5)
            {
                lives = Mathf.Min(lives + 1, 10);
            }
        }
        lives -= errorCount;
        // TODO: display dialogue
        gameState = GameState.FeedBack;
        
        // TODO: Gameover
    }
    public void StartNextQuack(){
        ToNextQuack();

    }
    public void SubmitCurrentQuack(){
        int errorCount = (userInput.GetComponent( typeof(KeyboardMixer) ) as KeyboardMixer).Submit();
        ProcessSubmission(errorCount);
        
    }
}
