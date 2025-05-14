using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSlot 
{
    protected Transform location;
    protected int level;

    public SpiritSlot(Transform location)
    {
        this.location = location;
    }

    public int Level { get { return level; } }

    

    
    public virtual bool CanUpgrade(SpiritSO spiritSO)
    {
        
        return true;
        
    }   
    public virtual void Upgrade()
    {
        level++;
    }    

    public virtual void SpawnSpirit()
    {

    }    
}
