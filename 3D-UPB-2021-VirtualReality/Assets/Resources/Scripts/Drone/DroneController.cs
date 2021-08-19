using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private const float BASE_HP = 4;
    private const float DEATH_TORQUE_FORCE = 200f;
    private const float MIN_DIST_TO_TARGET = 2f;
    private const float AGGRO_CHANCE = 0.25f;

    private AudioSource audioFlying;
    private DroneFlightController flightController;
    private DroneAttackController attackController;
    private Rigidbody rigidbody;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        audioFlying = GetComponent<AudioSource>();
        flightController = GetComponent<DroneFlightController>();
        attackController = GetComponent<DroneAttackController>();
        rigidbody = GetComponent<Rigidbody>();
        currentHP = BASE_HP;
    }

    private void Update()
    {
        // If close enough to target, despawn
        if (flightController.GetDistanceToTarget() < MIN_DIST_TO_TARGET)
            Destroy(gameObject);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
            Die();
        else
        // Check if should aggro
        if (Random.Range(0f, 1f) < AGGRO_CHANCE)
        {
            // Enable attack script
            attackController.enabled = true;
        }

    }

    private void Die()
    {
        // Disable flying sound
        audioFlying.Stop();

        // Make drone fall to ground
        rigidbody.isKinematic = false;

        // Give it some flair (spin it)
        rigidbody.AddTorque(new Vector3(Random.Range(0, DEATH_TORQUE_FORCE), Random.Range(0, DEATH_TORQUE_FORCE), Random.Range(0, DEATH_TORQUE_FORCE)), ForceMode.Impulse);

        // Destroy after a few seconds
        Destroy(gameObject, 8f);

        // Disable scripts
        flightController.enabled = false;
        attackController.enabled = false;
        enabled = false;
    }
}
