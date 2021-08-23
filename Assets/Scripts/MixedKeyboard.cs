using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mixed Keyboard", menuName = "Scriptable Objects/Mixed Keyboard", order = 0)]
public class MixedKeyboard : ScriptableObject
{
    public static char[] letters = new char[]
    { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M' };
    public Dictionary<char, char> charMap;
    [SerializeField]
    public string keyMapDoc;
    
    public void OnEnable()
    {
        Debug.Log("###Creating new mixed keyboard");
        keyMapDoc = "Key,Letter\n";
        charMap = new Dictionary<char, char>();
        char[] randomLetters = letters.OrderBy(x => Random.value).ToArray();
        for (int i = 0; i < letters.Length; i++)
        {
            charMap[letters[i]] = randomLetters[i];
            keyMapDoc += string.Format("{0},{1}\n", letters[i], randomLetters[i]);
        }
    }
}

