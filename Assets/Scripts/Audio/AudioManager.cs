using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("監聽音樂撥放")]
    public PlayAudioEventSo BGMEvent;
    public PlayAudioEventSo FXEvent;


    public AudioSource BGM;
    public AudioSource FX;

    private void OnEnable()
    {
        FXEvent.OnEventRise += OnFXEvent;
        BGMEvent.OnEventRise += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRise -= OnFXEvent;
        BGMEvent.OnEventRise -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FX.clip = clip;  
        FX.Play();
    }
}
