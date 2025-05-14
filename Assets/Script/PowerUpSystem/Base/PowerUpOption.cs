using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerUpOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]protected Image avatar;
    [SerializeField]protected TextMeshProUGUI title;
    protected GameObject pUUI;




    public void SetPUUI(GameObject pUUI)
    {
        this.pUUI = pUUI;
    }    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        pUUI.SetActive(false);
        Time.timeScale = 1.0f;
        
    }

  
}
