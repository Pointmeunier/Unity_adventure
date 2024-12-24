using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/PlayAudioEventSo")]
public class PlayAudioEventSo : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRise;

    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRise?.Invoke(audioClip);
    }
}
