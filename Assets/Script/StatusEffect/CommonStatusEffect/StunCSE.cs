using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StunCSE : CommonStatusEffect
{

    public StunCSE(Stats stats) : base(stats)
    {
    }

   

    protected override IEnumerator EffectCoroutine()
    {
        sTUIElement = stats.CreateSEUIElement();
        sTUIElement.sprite = IconsAndImages.Instance.StunnedIcon;
        isRun = true;
        elapsedTimes = 0.0f;
        stats.IsNotStunned = false;

        while (elapsedTimes < duration)
        {

            yield return new WaitForSeconds(1.0f);

            elapsedTimes += 1.0f;
        }
        stats.IsNotStunned = true;
        isRun = false;
        GameObject.Destroy(sTUIElement.gameObject);


    }
}
