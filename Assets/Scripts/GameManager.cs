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
        } else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        quacks = quackCollection.collection.OrderBy(x => Random.value).ToArray();
        quackIndex = -1;
        lives = 12;
        gameState = GameState.Quack;
        bossVoice.PlayRandom();
        healthBarSlider.value = lives;
        healthBarHandle.sprite = fullHealthHandle;
        ToNextQuack();
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
        quackIndex = (quackIndex + 1) % quacks.Length;
        currentQuack = quacks[quackIndex];
        textWriter.AddTextToWrite(dialogue, currentQuack.ToUpper(), .02f);
        nextQuackButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(true);
        gameOverImage.gameObject.SetActive(false);
        keyboard.isEnabled = true;
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
        if ((lives -= errorCount) > 0)
        {
            healthBarSlider.value = lives;
            lives -= errorCount;
            ToFeedback();
        }
        else if ((lives -= errorCount) <= 0)
        {
            ToGameOver();
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
        quackIndex = -1;
        lives = 12;
        healthBarSlider.value = lives;
        healthBarHandle.sprite = fullHealthHandle;
        keyboard.RemixKeyboard();
        timer.countTime = 310;
        timer.time = timer.countTime;
        gameOverImage.gameObject.SetActive(false);
        bgm.Play();
        ToNextQuack();
    }

    public void ToFeedback()
    {
        gameState = GameState.FeedBack;
        textWriter.AddTextToWrite(dialogue, feedback.ToUpper(), .02f);
        nextQuackButton.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
        keyboard.isEnabled = false;
    }

    public void ToGameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        bgm.Stop();
        keyboard.isEnabled = false;
    }
}
