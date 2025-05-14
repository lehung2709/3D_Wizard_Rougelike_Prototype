using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFalseObject : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    // Start is called before the first frame update
    
    
    private void Update()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
        Destroy(gameObject);
    }


}
