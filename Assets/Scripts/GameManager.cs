using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    public Button submitButton;
    public enum GameState
    {
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
        quacks = quackCollection.collection.OrderBy(x => Random.value).ToArray();
        currentQuack = quacks[quackIndex];
        lives = 10;
        gameState = GameState.Quack;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.FeedBack:
                dialogue.text = feedback.ToUpper();
                nextQuackButton.gameObject.SetActive(true);
                submitButton.gameObject.SetActive(false);

                break;
            case GameState.Quack:
                dialogue.text = currentQuack.ToUpper();
                nextQuackButton.gameObject.SetActive(false);
                submitButton.gameObject.SetActive(true);

                break;
            default:
                dialogue.text = "this is a default dialogue.".ToUpper();
                nextQuackButton.gameObject.SetActive(false);
                break;
        }
    }

    public void ToNextQuack()
    {
        gameState = GameState.Quack;
        userInput.text = "";
        quackIndex = (quackIndex + 1) % quacks.Length;
        currentQuack = quacks[quackIndex];
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
        gameState = GameState.FeedBack;
    }
}
