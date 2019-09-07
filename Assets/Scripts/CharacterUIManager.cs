using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null)
        {
            Debug.Log("Camera null");
        } else
        {
            transform.LookAt(Camera.main.transform);
        }
    }

}
