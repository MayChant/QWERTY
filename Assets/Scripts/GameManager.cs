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
    public KeyboardMixer keyboard;
    public Text dialogue;
    public Text userInput;
    public Button nextQuackButton;
    public Button submitButton;
    public Slider healthBarSlider;
    public Image healthBarHandle;
    public Sprite fullHealthHandle;
    public Sprite halfHealthHandle;
    public Sprite noHealthHandle;
    public Image gameOverImage;
    public BossVoice bossVoice;
    public enum GameState
    {
        Intro,
        Quack,
        FeedBack,
        Ending,
        GameOver,
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
        lives = 12;
        gameState = GameState.Quack;
        healthBarSlider.value = lives;
        healthBarHandle.sprite = fullHealthHandle;
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
                gameOverImage.gameObject.SetActive(false);
                keyboard.isEnabled = false;

                break;
            case GameState.Quack:
                dialogue.text = currentQuack.ToUpper();
                nextQuackButton.gameObject.SetActive(false);
                submitButton.gameObject.SetActive(true);
                gameOverImage.gameObject.SetActive(false);
                keyboard.isEnabled = true;

                break;
            case GameState.GameOver:
                gameOverImage.gameObject.SetActive(true);
                keyboard.isEnabled = false;

                break;
            default:
                dialogue.text = "this is a default dialogue.".ToUpper();
                nextQuackButton.gameObject.SetActive(false);
                gameOverImage.gameObject.SetActive(false);
                keyboard.isEnabled = false;
                break;
        }
    }

    public void ToNextQuack()
    {
        gameState = GameState.Quack;
        userInput.text = "";
        quackIndex = (quackIndex + 1) % quacks.Length;
        currentQuack = quacks[quackIndex];
        bossVoice.PlayRandom();
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
            bossVoice.PlayGoodJob();
        }
        else
        {
            bossVoice.PlayRandom();
        }
        //lives -= errorCount;
        if ((lives -= errorCount) > 0)
        {
            healthBarSlider.value = lives;
            gameState = GameState.FeedBack;
            lives -= errorCount;
        }
        else if ((lives -= errorCount) <= 0)
        {
            gameState = GameState.GameOver;
        }
        RenderHealthBarHandle();
    }

    private void RenderHealthBarHandle()
    {
        if (lives > 8)
        {
            healthBarHandle.sprite = fullHealthHandle;
        }
        else if (lives > 4)
        {
            healthBarHandle.sprite = halfHealthHandle;
        }
        else
        {
            healthBarHandle.sprite = noHealthHandle;
        }
    }

    public void Initialize()
    {
        quacks = quackCollection.collection.OrderBy(x => Random.value).ToArray();
        currentQuack = quacks[quackIndex];
        lives = 12;
        gameState = GameState.Quack;
        healthBarSlider.value = lives;
        healthBarHandle.sprite = fullHealthHandle;
        keyboard.RemixKeyboard();
    }
}
