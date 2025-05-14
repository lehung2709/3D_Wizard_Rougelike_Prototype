using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class LevelOption : MonoBehaviour,IPointerClickHandler
{
    private int level;
    private MapAndLevelManager mapAndLevelManager;
    [SerializeField]private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    

    public void SetMapAndLevelManager(MapAndLevelManager mapAndLevelManager)
    {
        this.mapAndLevelManager = mapAndLevelManager;
    }
    public void SetLevel(int level)
    {
        this.level = level;
        text.text=level.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mapAndLevelManager.LoadScene(level);
    }
}
