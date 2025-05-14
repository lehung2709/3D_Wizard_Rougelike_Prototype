using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapAndLevelManager : MonoBehaviour
{
    [SerializeField] private MapOption[] mapOptions;
    private MapOption currentMapOption;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private Transform levelsContainer;
    [SerializeField] private SpawnData commonSpawnData;
    [SerializeField] private LevelOption levelOptionPrefab;
    private List<LevelOption> levelOptiones;
    private void Awake()
    {
        foreach (var option in mapOptions)
        {
            option.SetMapAndLevelManager(this);
        }
        levelOptiones = new List<LevelOption>();

    }
    public void LoadScene(int level)
    {
        SpawnData spawnData = currentMapOption.GetSpawnData(level-1);
        commonSpawnData.maxDifficultyPoint = spawnData.maxDifficultyPoint;
        commonSpawnData.enemySOs = spawnData.enemySOs;
        commonSpawnData.difficultyPointIncreaseRate = spawnData.difficultyPointIncreaseRate;
        commonSpawnData.spawnInterval=spawnData.spawnInterval;
        commonSpawnData.spawnIntervalIncreaseRate = spawnData.spawnIntervalIncreaseRate;

        SceneManager.LoadScene(currentMapOption.SceneName);
    }
    public void SetCurentMap(MapOption mapOption)
    {
        levelsPanel.SetActive(true);

        this.currentMapOption = mapOption;
        if(levelOptiones.Count<currentMapOption.LevelCount )
        {
            for (int i = 0; i < levelOptiones.Count; i++)
            {
                levelOptiones[i].gameObject.SetActive(true);
            }
            for (int i = levelOptiones.Count+1; i <= currentMapOption.LevelCount; i++)
            {
                LevelOption newLevelOption = Instantiate(levelOptionPrefab);
                newLevelOption.transform.SetParent(levelsContainer);
                newLevelOption.SetMapAndLevelManager(this);
                newLevelOption.SetLevel(i);
                levelOptiones.Add(newLevelOption);
                
            }
            return;
          
        }  
        for(int i = 0; i < currentMapOption.LevelCount; i++)
        {
            levelOptiones[i].gameObject.SetActive(true);
        }    
    }  

    public void CloseLevelPanel()
    {
        for (int i = 0; i < currentMapOption.LevelCount; i++)
        {
            levelOptiones[i].gameObject.SetActive(false);
        }
        levelsPanel.SetActive(false);
    }    
    
        

     
    
}
