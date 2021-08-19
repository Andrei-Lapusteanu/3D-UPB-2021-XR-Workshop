using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private const float WEAPON_FIRE_RATE = 0.16f;
    public static int WEAPON_STATE_IDLE = 0;
    public static int WEAPON_STATE_FIRE = 1;

    public GameObject projectilePrefab;

    private AudioSource audioFire;
    private Animator animator;
    private float nextWeaponFire = 0f;
    private GameObject muzzleEnd;

    // Start is called before the first frame update
    void Start()
    {
        audioFire = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        muzzleEnd = transform.Find("MuzzleEnd").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // If fire button is presses button
        if (Input.GetButton("Fire1") || Input.GetButton("Joystick Button B"))
        {
            if(Time.time > nextWeaponFire)
            {
                // Set a random rotation for the projectile
                Quaternion randRotation = new Quaternion();
                randRotation.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                
                // Instantiate projectile
                Instantiate(projectilePrefab, muzzleEnd.transform.position, randRotation, muzzleEnd.transform);

                // Play sound
                PlayFireSound();

                // Update fire rate
                nextWeaponFire = Time.time + (WEAPON_FIRE_RATE * 1.6666f);

                // Play fire animation
                UpdateAnimState(WEAPON_STATE_FIRE);
            }
        }
        else
        {
            // Idle anim
            UpdateAnimState(WEAPON_STATE_IDLE);
        }
    }

    private void PlayFireSound()
    {
        // Randomize audio pitch (sound is less annoying/boring over time)
        audioFire.pitch = Random.Range(0.8f, 1.1f);
        audioFire.Play();
    }

    private void UpdateAnimState(int stateVal)
    {
        animator.SetInteger("WeaponState", stateVal);
    }
}
