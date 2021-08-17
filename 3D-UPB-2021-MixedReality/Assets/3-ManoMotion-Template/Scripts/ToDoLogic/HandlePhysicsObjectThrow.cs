using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePhysicsObjectThrow : MonoBehaviour
{
    Rigidbody rigidbody;
    UpdatePhysicsObjectThrowPower throwPowerScript;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        throwPowerScript = GetComponent<UpdatePhysicsObjectThrowPower>();
    }

    public void ComputeThrowAction()
    {
        /* TODO: Get throw direction
         *       Hint: CameraInputController already has 'CameraRay' computed - which is ray cast from screen pos to world dir
         *       Apply force as velocity (or experiment wiht rigidbody.AddForce()) to rigidbody (basketball). 
         *       Don't forget to get throw power from 'throwPowerScript'
         */
    }
}
