using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawner : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    private GameObject instantiatedPrefab;

    public void SpawnPrefab()
    {
        // Spawn as a child of the AR scene
        instantiatedPrefab = Instantiate(PrefabToSpawn, transform.position, Quaternion.identity, transform.parent);

        // Move it just so prefabs won't stack on eachother
        float jiggleAmout = Random.Range(-0.02f, 0.02f);
        instantiatedPrefab.transform.position += new Vector3(jiggleAmout, jiggleAmout, jiggleAmout);
    }
}
