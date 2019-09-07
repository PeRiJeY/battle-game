using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursivePreventSelfCollisions : MonoBehaviour
{

    //Go through each child and set its collider to ignore all other colliders in our gameObject
    void Awake()
    {
        recursivePreventSelfCollisions(transform, transform);
    }

    private void recursivePreventSelfCollisions(Transform parentTransform, Transform actualTransform)
    {
        Collider collider = parentTransform.GetComponent<Collider>();
        if (collider == null) return;
                    
        foreach (Transform child in actualTransform)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                // Disable a collision
                Physics.IgnoreCollision(collider, childCollider);
            } 

            recursivePreventSelfCollisions(parentTransform, child);
        }
    }
}
