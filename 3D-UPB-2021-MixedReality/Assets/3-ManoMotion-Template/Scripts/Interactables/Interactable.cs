using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

// Base interactable class
public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected Material highlightMaterial;
    protected Material originalMaterial;
    protected bool isInteractedWith;
    protected new Rigidbody rigidbody;
    protected new Collider collider;

    public InteractableType InteractableType;

    // Start is called before the first frame update
    void Awake()
    {
        originalMaterial = GetComponent<Renderer>().material;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        isInteractedWith = false;
    }

    private void Update()
    {
        // If object is currently interacted with, set its highlight material
        if (isInteractedWith == true)
            SetHighlightMaterial();
    }

    public void SetHighlightMaterial()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void SetOriginalMaterial()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        // When hand is outside of object's range, set its original material
        SetOriginalMaterial();
    }

    public bool IsInteractedWith { get => isInteractedWith; set => isInteractedWith = value; }
}
