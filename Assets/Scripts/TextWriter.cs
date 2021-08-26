using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextWriter : MonoBehaviour
{
    private Text text;
    private string textToWrite; 
    private int characterIndex; 
    private float timePerCharacter;
    private float timer;

    public void AddTextToWrite(Text text, string textToWrite, float timerPerCharacter)
    {
        this.text = text;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(text != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                //display next character
                timer += timePerCharacter;
                characterIndex++;
                text.text = textToWrite.Substring(0, characterIndex);
                if(characterIndex >= textToWrite.Length)
                {
                    text = null;
                    return;
                }
            }
        }
    }
}
