using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : Box {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private new void OnCollisionEnter(Collision collision)
    {
        HurtBox hurtbox = collision.gameObject.GetComponent<HurtBox>();
        if (hurtbox == null) return;

        // Debug.Log("Hit to: " + collision.collider.name);
    }
}
