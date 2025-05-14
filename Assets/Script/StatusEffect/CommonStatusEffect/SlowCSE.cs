using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SlowCSE : CommonStatusEffect
{

    public SlowCSE(Stats stats) : base(stats)
    {
    }

    protected override IEnumerator EffectCoroutine()
    {
        sTUIElement = stats.CreateSEUIElement();
        sTUIElement.sprite = IconsAndImages.Instance.SlowedDownIcon;
        isRun = true;
        elapsedTimes = 0.0f;
        stats.MoveSpeed *= intensity;

        while (elapsedTimes < duration)
        {
            yield return new WaitForSeconds(1.0f);

            elapsedTimes += 1.0f;
        }
        stats.MoveSpeed *= (1/intensity);
        isRun = false;
        GameObject.Destroy(sTUIElement.gameObject);


    }
}
