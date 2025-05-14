using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsManager : MonoBehaviour
{
    private List<SpiritSlot> spiritSlots;
    [SerializeField] private Transform[] locations;
    private int nextEmptyLocatinIndex=0;


    private void Awake()
    {
        spiritSlots = new List<SpiritSlot>();
    }
    

    public void TakeSpirit(SpiritSO spiritSO,int slotIndex)
    {
        if (slotIndex > -1)
            spiritSlots[slotIndex].Upgrade();
        else
        {
            if (spiritSO is OffensiveSpiritSO)
            {
                spiritSlots.Add(new OffensiveSpiritSlot(locations[nextEmptyLocatinIndex], (OffensiveSpiritSO)spiritSO));
                spiritSlots[nextEmptyLocatinIndex].SpawnSpirit();


            }
        }
        nextEmptyLocatinIndex++;
   
    }

    
    public int GetIndex(SpiritSO spiritSO)
    {
        
        for(int i = 0; i < spiritSlots.Count; i++)
        {
            if (spiritSlots[i].CanUpgrade(spiritSO))return i;
        } 
        return -1;
            
    }    
    public int GetLevel(int index)
    {
        return spiritSlots[index].Level+1;
    }    
    public bool IsFull()
    {
        if (nextEmptyLocatinIndex > locations.Length - 1) return true;
        return false;
    }    

}
