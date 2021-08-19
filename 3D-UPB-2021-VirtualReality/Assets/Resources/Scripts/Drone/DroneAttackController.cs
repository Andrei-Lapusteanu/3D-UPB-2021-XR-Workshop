using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackController : MonoBehaviour
{
    // TODO: Refer to git for more details on implementation

    public GameObject droneProjectile;

    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        // TODO: Disable the drone flight controller
    }

    void Update()
    {
        // TODO: Drone attack logic/behaviour
    }

    private void PlayFireSound()
    {
        // Randomize audio pitch (sound is less annoying/boring over time)
        audioSources[1].pitch = Random.Range(0.8f, 1.1f);
        audioSources[1].Play();
    }
}
