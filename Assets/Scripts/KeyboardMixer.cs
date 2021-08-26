using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMixer : MonoBehaviour
{
    public Dictionary<char, char> charMap;
    public MixedKeyboard[] mixedKeyboards;
    public AudioSource audioSource;
    public AudioClip correct;
    public AudioClip wrong;
    private Text text;
    private GameManager gameManager;

    public bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        foreach (MixedKeyboard keyboard in mixedKeyboards)
        {
            keyboard.charMap = new Dictionary<char, char>();
            string[] lines = keyboard.keyMapDoc.Split('\n');
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] pair = lines[i].Split(',');
                keyboard.charMap[pair[0].ToCharArray()[0]] = pair[1].ToCharArray()[0];
            }
        }
        RemixKeyboard();
        text = GetComponent<Text>();
        isEnabled = true;
    }

    public void RemixKeyboard()
    {
        int keyboardIndex = Random.Range(0, mixedKeyboards.Length);
        Debug.Log(keyboardIndex);
        charMap = mixedKeyboards[keyboardIndex].charMap;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessNewKey();
    }

    public void ProcessNewKey()
    {
        if (!isEnabled)
        {
            return;
        }
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (text.text.Length != 0)
                {
                    text.text = text.text.Substring(0, text.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                ChooseSubmit();
            }
            else
            {
                text.text += GetInputChar(c);
            }
        }
    }

    public char GetInputChar(char newChar)
    {
        charMap.TryGetValue(char.ToUpper(newChar), out char realChar);
        if (realChar != default)
        {
            return realChar;
        }
        return newChar;
    }

    public void ChooseSubmit()
    {
        gameManager.ProcessSubmission(Submit());
    }

    public int Submit()
    {
        // Return error count
        // Return error count
        gameManager.feedback = "";
        string submission = text.text.ToUpper();
        string currentQuack = gameManager.currentQuack.ToUpper();
        if (submission.Equals(currentQuack))
        {
            gameManager.feedback = "Good job.";
            audioSource.clip = correct;
            audioSource.Play();
            return 0;
        }
        int errorCount = 0;
        string[] submissionWords = submission.Split(' ');
        string[] currentQuackWords = currentQuack.Split(' ');
        foreach (string quackWord in currentQuackWords)
        {
            if (!submissionWords.Contains(quackWord))
            {
                if (gameManager.feedback.Equals(""))
                {
                    gameManager.feedback = string.Format("I said \"{0}\", intern. Are you even listening?", quackWord);
                }
                errorCount++;
            }
        }
        foreach (string submissionWord in submissionWords)
        {
            if (!currentQuackWords.Contains(submissionWord))
            {
                // TODO: compain about first error
                if (gameManager.feedback.Equals(""))
                {
                    gameManager.feedback = string.Format("Who told you to send \"{0}\"? Not me, intern. Not me.", submissionWord);
                }
                errorCount++;
            }
        }
        audioSource.clip = wrong;
        audioSource.Play();
        return errorCount;
    }
}
