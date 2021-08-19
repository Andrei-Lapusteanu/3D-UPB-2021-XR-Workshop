using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectileController : MonoBehaviour
{
    // TODO: Refer to git for more details on implementation

    void Start()
    {
        // TODO: Projectile logic/behaviour

    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Give damage to player


        Destroy(gameObject);
    }
}
