using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackGround : MonoBehaviour
{
    [SerializeField] private RawImage _img; 
    [SerializeField] private float _x, _y;  

    private void FixedUpdate()
    {
        _img.uvRect = new Rect(
            _img.uvRect.position + new Vector2(_x, _y) * Time.fixedDeltaTime,
            _img.uvRect.size
        );
    }
}
