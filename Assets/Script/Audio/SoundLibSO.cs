using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library")]
public class SoundLibSO : ScriptableObject
{
    [SerializeField]
    private List<SoundData> sounds; // List to hold all sound data.

    private Dictionary<string, SoundData> soundDictionary; // Dictionary for quick lookup by name.

    private void OnEnable()
    {
        
        soundDictionary = new Dictionary<string, SoundData>();
        foreach (var sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.soundName))
            {
                soundDictionary.Add(sound.soundName, sound);
            }
            
        }
    }

    
    public SoundData GetSound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out var soundData))
        {
            return soundData;
        }

        Debug.LogError($"Sound '{soundName}' not found in library.");
        return null;
    }
}
