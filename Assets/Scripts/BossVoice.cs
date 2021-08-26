using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVoice : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] lines;
    public AudioClip goodLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandom()
    {
        audioSource.clip = lines[Random.Range(0, lines.Length)];
        audioSource.Play();
    }

    public void PlayGoodJob()
    {
        audioSource.clip = goodLine;
        audioSource.Play();
    }
}
