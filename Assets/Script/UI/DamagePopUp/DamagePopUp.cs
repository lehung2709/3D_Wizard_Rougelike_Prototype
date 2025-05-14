using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private AnimationCurve opacityCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve heightCurve;


    private TextMeshProUGUI tmp;
    private float time = 0;
    private Vector3 origin;

    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        Color currentColor = tmp.color;
        tmp.color = new Color(currentColor.r, currentColor.g, currentColor.b, opacityCurve.Evaluate(time));
        transform.localScale=Vector3.one*scaleCurve.Evaluate(time);
        transform.position=origin+new Vector3(0.0f,1+heightCurve.Evaluate(time),0.0f);
        time += Time.fixedDeltaTime;
        if (time > 1.0f)
        {
            time = 0.0f;
            DamagePopUpGenerator.Instance.ReturnToPool(this);
        }
    }
    public void SetPos(Vector3 pos)
    {
        Vector3 randomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));

        origin = pos+randomness;
        transform.position = origin;
    }    

}
