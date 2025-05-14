using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator Instance;
    [SerializeField] private DamagePopUp dpPrefab;
    [SerializeField] private GameObject disapearEffectPrefab;

    private Queue<DamagePopUp> damagePopUpPool = new Queue<DamagePopUp>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    public void CreatePopUp(Vector3 position,string text)
    {
        DamagePopUp popup;

        if (damagePopUpPool.Count > 0)
        {
            popup = damagePopUpPool.Dequeue();
            popup.gameObject.SetActive(true);
        }
        else
        {
            popup = Instantiate(dpPrefab);
        }
        popup.SetPos(position);
        var temp=popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text=text;
    }

    public void ReturnToPool(DamagePopUp dp)
    {
        dp.gameObject.SetActive(false);
        damagePopUpPool.Enqueue(dp);
    }
    public void CreateDisapearEffect(Vector3 position)
    {
        Instantiate(disapearEffectPrefab,position,Quaternion.identity);
    }    

}
