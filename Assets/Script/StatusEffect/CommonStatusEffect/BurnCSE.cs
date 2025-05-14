using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BurnCSE : CommonStatusEffect
{
    
    public BurnCSE(Stats stats) : base(stats)
    {
    }

    protected override IEnumerator EffectCoroutine()
    {
        sTUIElement = stats.CreateSEUIElement();
        sTUIElement.sprite=IconsAndImages.Instance.BurnedIcon;
        isRun = true;
        elapsedTimes = 0.0f;

        while (elapsedTimes < duration)
        {
            stats.TakeDamage(intensity);

            yield return new WaitForSeconds(1.0f);

            elapsedTimes += 1.0f;
        }
        isRun = false;
        GameObject.Destroy(sTUIElement.gameObject);
        
  
    }
}
