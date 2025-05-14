using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    [SerializeField]private Transform cam;

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);

    }
}
