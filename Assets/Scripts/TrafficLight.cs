using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public float timeIn = 15.0f;
    public float timeOut = 30.0f;
    public float timeElapsed;
    public bool stopGo;
    public AudioClip audioClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    audioSource = gameObject.GetComponent<AudioSource>();
    audioSource.clip = audioClip;
    stopGo = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeIn){
            stopGo = true;      //Go
            audioSource.PlayOneShot(audioClip);    
            // Debug.Log("Play sound");        
            if(timeElapsed >= timeOut){
                stopGo = false;         //Stop
                audioSource.Stop();
                timeElapsed = 0.0f;
            }
        }
    }
}
