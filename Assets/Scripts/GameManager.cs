using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public QuackCollection quackCollection;
    public QuackCollection introCollection;
    private string[] quacks;
    private string[] intro;
    private int quackIndex;
    private int introIndex = 0;
    public string currentQuack;
    public string currentIntro;
    public string feedback;
    public int lives;
    public int streak;
    public bool isGivingFeedback;
    public KeyboardMixer keyboard;
    public Text dialogue;
    public Text userInput;
    public Button nextQuackButton;
    public Button submitButton;
    public Button dialogueButton;
    public Button skipIntroButton;
    public Slider healthBarSlider;
    public Image healthBarHandle;
    public Sprite fullHealthHandle;
    public Sprite halfHealthHandle;
    public Sprite noHealthHandle;
    public Image gameOverImage;
    public BossVoice bossVoice;
    public Timer timer;
    public AudioSource bgm;
    [SerializeField] private TextWriter textWriter;
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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        quacks = quackCollection.collection.OrderBy(x => Random.value).ToArray();
        quackIndex = -1;
        introIndex = -1;
        SetLives(12);
        healthBarHandle.sprite = fullHealthHandle;
        intro = introCollection.collection;
        ToIntro();
    }

    private void ToIntro()
    {
        gameState = GameState.Intro;
        nextQuackButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        dialogueButton.gameObject.SetActive(false);
        skipIntroButton.gameObject.SetActive(true);
        keyboard.isEnabled = false;
        NextDialogue();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOverImage.gameObject.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Initialize();
            }
        }
    }

    public void ToNextQuack()
    {
        gameState = GameState.Quack;
        bossVoice.PlayRandom();
        userInput.text = "";
        keyboard.RemixKeyboard();
        quackIndex = (quackIndex + 1) % quacks.Length;
        currentQuack = quacks[quackIndex];
        textWriter.AddTextToWrite(dialogue, currentQuack.ToUpper(), .02f, submitButton);
        nextQuackButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(true);
        gameOverImage.gameObject.SetActive(false);
        dialogueButton.gameObject.SetActive(false);
        skipIntroButton.gameObject.SetActive(false);
        keyboard.isEnabled = true;
    }

    public void ProcessSubmission(int errorCount)
    {
        if (errorCount == 0)
        {
            streak++;
            if (streak == 5)
            {
                SetLives(Mathf.Min(lives + 1, 10));
            }
            bossVoice.PlayGoodJob();
        }
        else
        {
            bossVoice.PlayRandom();
        }
        SetLives(lives - errorCount);
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
        quackIndex = -1;
        introIndex = -1;
        SetLives(12);
        keyboard.RemixKeyboard();
        timer.countTime = timer.initialCountTime;
        timer.time = timer.countTime;
        gameOverImage.gameObject.SetActive(false);
        bgm.Play();
        ToIntro();
    }

    public void ToFeedback()
    {
        gameState = GameState.FeedBack;
        textWriter.AddTextToWrite(dialogue, feedback.ToUpper(), .02f, nextQuackButton);
        submitButton.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        skipIntroButton.gameObject.SetActive(false);
        keyboard.isEnabled = false;
    }

    public void ToGameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        bgm.Stop();
        keyboard.isEnabled = false;
    }
    public void NextDialogue()
    {
        dialogueButton.gameObject.SetActive(false);
        if((introIndex + 1) >= intro.Length)
        {
            //gameManager.gameState = GameManager.GameState.Quack;
            ToNextQuack(); 
        }
        else
        {
            introIndex += 1;
            currentIntro = intro[introIndex];
            if(gameState == GameState.Intro)
            {
                textWriter.AddTextToWrite(dialogue, currentIntro, 0.02f, dialogueButton);
            }       
                
        }
    }

    private void SetLives(int number)
    {
        lives = number;
        if (lives > 0)
        {
            ToFeedback();
        }
        else if (lives <= 0)
        {
            ToGameOver();
        }
        healthBarSlider.value = lives;
        RenderHealthBarHandle();
    }
}
