using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonProjectileController : MonoBehaviour
{
    private const float PROJECTILE_VELOCITY = 25f;
    private const float BALLOON_DAMAGE = 1f;

    public GameObject particlesSplash;

    private Rigidbody rigidbody;
    private Vector3 projectileTargetDir;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        // The target is the forward of the cannon's muzzle end (parent of this)
        projectileTargetDir = gameObject.transform.parent.transform.forward;
        // Ray camToWorldRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f), Camera.MonoOrStereoscopicEye.Mono);
        // projectileTargetDir = camToWorldRay.direction;

        // Apply a physical impulse to the objects
        rigidbody.AddForce(projectileTargetDir * PROJECTILE_VELOCITY, ForceMode.Impulse);

        // Unparent, otherwise it will move relative to the muzzle end
        transform.parent = null;

        // Destroy after a few seconds (keep object count managable)
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Config particle
        particlesSplash.transform.position = collision.GetContact(0).point;
        particlesSplash.transform.localRotation = Quaternion.LookRotation(collision.GetContact(0).normal);

        // Play splash particles
        GameObject particleInst = Instantiate(particlesSplash);
        particleInst.GetComponent<ParticleSystem>().Play();

        // If drone is hit, give damage to it
        if (collision.gameObject.GetComponent<DroneController>() != null)
            collision.gameObject.GetComponent<DroneController>().TakeDamage(BALLOON_DAMAGE);

        Destroy(particleInst.gameObject, 1.5f);
        Destroy(gameObject);
    }
}
