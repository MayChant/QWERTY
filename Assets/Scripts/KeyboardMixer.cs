using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMixer : MonoBehaviour
{
    public Dictionary<char, char> charMap;
    public MixedKeyboard[] mixedKeyboards;
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
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
                // submit
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
}
