using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Enter -> Receptor: " + gameObject.name + ", Emisor: " + other.gameObject.name);
    }
    protected void OnCollisionExit(Collision collision)
    {
        // Debug.Log("Exit -> Receptor: " + gameObject.name + ", Emisor: " + other.gameObject.name);
    }
}
