using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextWriter : MonoBehaviour
{
   
    private Text text;
    private string textToWrite; 
    private int characterIndex; 
     [SerializeField]
    private float timePerCharacter;
    private float timer;
    private bool writing;
    private Button controlButton;

    public void AddTextToWrite(Text text, string textToWrite, float timePCharacter, Button controlButton)
    {
        this.text = text;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePCharacter;
        this.controlButton = controlButton;
        writing = true;
        characterIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (writing)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                //display next character
                timer += timePerCharacter;
                characterIndex++;
                if (characterIndex > textToWrite.Length)
                {
                    text = null;
                    writing = false;
                    controlButton.gameObject.SetActive(true);
                    return;
                }
                text.text = textToWrite.Substring(0, characterIndex);
            }
        }
    }
}
