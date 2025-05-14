using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonStatusEffect : StatusEffect
{
    protected Stats stats;

    public CommonStatusEffect(Stats stats)
    {
        this.stats = stats;
    }

    public override void StartStatusEffect(float Intensity, float duration)
    {
        base.StartStatusEffect(Intensity, duration);
        if (isRun)
        {
            elapsedTimes = 0.0f;
        }
        else
        {
            if(stats.isActiveAndEnabled) 
            stats.StartCoroutine(EffectCoroutine());

        }
    }

    protected virtual IEnumerator EffectCoroutine()
    {
       return null;

    }
}
