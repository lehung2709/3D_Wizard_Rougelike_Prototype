using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private SoundLibSO soundLibSO;

    [SerializeField] private Queue<AudioEmitter> audioEmitterPool;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private string musicName;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            InitializePool();
            PlayMusic(musicName);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        audioEmitterPool = new Queue<AudioEmitter>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateSoundEmitter();
        }
    }

    private AudioEmitter CreateSoundEmitter()
    {
        GameObject soundEmitterObject = new GameObject("SoundEmitter");
        AudioEmitter emitter = soundEmitterObject.AddComponent<AudioEmitter>();
        soundEmitterObject.SetActive(false);
        audioEmitterPool.Enqueue(emitter);
        return emitter;
    }

    public AudioEmitter SpawnSoundEmitter( Transform parent,string soundName,Vector3 pos)
    {
        AudioEmitter audioEmitter;

        if (audioEmitterPool.Count > 0)
        {
            audioEmitter = audioEmitterPool.Dequeue();
        }
        else
        {
            audioEmitter = CreateSoundEmitter();
        }

        audioEmitter.transform.SetParent(parent);
        audioEmitter.transform.position = pos;
        audioEmitter.gameObject.SetActive(true); 

        audioEmitter.PlaySound(soundLibSO.GetSound(soundName));
        return audioEmitter;
    }

    public void ReturnToPool(AudioEmitter emitter)
    {
        audioEmitterPool.Enqueue(emitter);
    }

    

    public void PlayBtnSound()
    {
        SpawnSoundEmitter(transform, "Btn",Vector3.zero);
    }
    
    public void PlayMusic(string musicName)
    {
        this.musicName = musicName;
        SpawnSoundEmitter(null, musicName, Vector3.zero);

    }    
}
