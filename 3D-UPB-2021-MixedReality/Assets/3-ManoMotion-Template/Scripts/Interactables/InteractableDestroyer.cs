using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If it's a physics object
        if(other.gameObject.layer == Constants.LAYER_PHYSICS_OBJECT)
        {
            // Let the object handle its own demise
            other.GetComponent<InteractableMovable>().HandleDestroy();
        }
    }
}
