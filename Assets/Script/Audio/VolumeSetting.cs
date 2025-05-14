using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    private Slider slider;
    [SerializeField] private string groupName;
    [SerializeField] private float maxdB;


    private void Awake()
    {
        slider = GetComponent<Slider>();
        
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(groupName))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }


    public void SetVolume()
    {
        float volume = slider.value;
        if (volume == 0) volume = 0.001f;
        myMixer.SetFloat(groupName, Mathf.Log10(volume) * 20+maxdB);
        PlayerPrefs.SetFloat(groupName, volume);
    }

    private void LoadVolume()
    {
        slider.value = PlayerPrefs.GetFloat(groupName);
        SetVolume();
    }

}
