using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapOption : MonoBehaviour,IPointerClickHandler
{
    private MapAndLevelManager mapAndLevelManager;
    [SerializeField] private SpawnData[] spawnDatas;
    [SerializeField] private string sceneName;
    public string SceneName {  get { return sceneName; } }
    public int LevelCount {  get { return spawnDatas.Length; } }

    public void SetMapAndLevelManager(MapAndLevelManager mapAndLevelManager)
    {
        this.mapAndLevelManager = mapAndLevelManager;
    }    
    public SpawnData GetSpawnData(int level)
    {
        return spawnDatas[level];
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        mapAndLevelManager.SetCurentMap(this);
        AudioManager.Instance.SpawnSoundEmitter(null, "Btn",Vector3.zero);
    }
}
