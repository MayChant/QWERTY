using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMixer : MonoBehaviour
{
    public Dictionary<char, char> charMap;
    public MixedKeyboard[] mixedKeyboards;
    private Text text;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        charMap = mixedKeyboards[Random.Range(0, mixedKeyboards.Length)].charMap;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessNewKey();
    }

    public void ProcessNewKey()
    {
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
            return 0;
        }
        int errorCount = 0;
        string[] submissionWords = submission.Split(' ');
        string[] currentQuackWords = currentQuack.Split(' ');
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
        return errorCount;
    }
}
