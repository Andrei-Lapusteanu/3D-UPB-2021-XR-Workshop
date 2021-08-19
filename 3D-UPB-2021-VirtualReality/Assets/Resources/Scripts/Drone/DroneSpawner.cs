using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public GameObject[] Drones;
    public GameObject[] DroneTargets;

    private const float SPAWN_RATE_MIN = 5f;
    private const float SPAWN_RATE_MAX = 40f;

    private GameObject randPrefab = null;
    private GameObject randTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        // Start asynchronous routine
        StartCoroutine(SpawnDronesAsync());
    }

    IEnumerator SpawnDronesAsync()
    {
        for(; ;)
        {
            yield return new WaitForSeconds(Random.Range(SPAWN_RATE_MIN, SPAWN_RATE_MAX));

            // Select random drone
            randPrefab = Drones[Random.Range(0, Drones.Length)];

            // Select random target
            randTarget = DroneTargets[Random.Range(0, DroneTargets.Length)];

            Instantiate(randPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public Vector3 GetDroneTargetPosition()
    {
        return randTarget.transform.position;
    }
}
