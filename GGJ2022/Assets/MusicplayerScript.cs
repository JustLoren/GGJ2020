using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicplayerScript : MonoBehaviour
{
    public AudioSource DystopianAudio;
    public AudioSource MusicboxAudio;
   

    private void Start()
    {
        MusicboxAudio.enabled = false;
        DystopianAudio.enabled = true;
  
    }
    private void OnTriggerEnter(Collider Player)
    {
        MusicboxAudio.enabled = true;
        DystopianAudio.enabled = false;
    }
    private void OnTriggerExit(Collider Player)
    {
        MusicboxAudio.enabled = false;
        DystopianAudio.enabled = true;
    }
    private void OnTriggerStay(Collider  Player)
    {
        
    }
}
   
