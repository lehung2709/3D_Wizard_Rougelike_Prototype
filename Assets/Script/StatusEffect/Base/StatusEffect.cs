using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class StatusEffect 
{
    protected float intensity;
    protected float duration;
    protected float elapsedTimes = 0.0f;
    protected bool isRun;
    protected Image sTUIElement;
    public virtual void StartStatusEffect(float Intensity,float duration)
    {
        this.intensity = Intensity;
        this.duration = duration;
    }
    
   
}
