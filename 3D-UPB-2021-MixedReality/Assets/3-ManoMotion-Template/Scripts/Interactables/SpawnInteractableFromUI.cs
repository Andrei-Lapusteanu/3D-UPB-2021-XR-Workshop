using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInteractableFromUI : MonoBehaviour
{
    public void SpawnInteractable()
    {
        GameObject.Find("InteractableSpawner").GetComponent<InteractableSpawner>().SpawnPrefab();
    }
}
