using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmitter : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void PlaySound(SoundData soundData)
    {
        if(soundData == null) StopAndReturnToPool();
        audioSource.clip = soundData.clip;
        audioSource.outputAudioMixerGroup = soundData.mixerGroup;
        audioSource.loop = soundData.loop;
        audioSource.Play();

        if (!soundData.loop)
        {
            StartCoroutine(DeactivateAfterPlay(soundData.clip.length));
        }
    }

    private IEnumerator DeactivateAfterPlay(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopAndReturnToPool();
    }

    public void StopAndReturnToPool()
    {
        audioSource.Stop();
        gameObject.SetActive(false);
        AudioManager.Instance.ReturnToPool(this);
    }
}
