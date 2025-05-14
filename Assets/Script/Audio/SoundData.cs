using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundData 
{
    public string soundName;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    public bool loop = false;

}
