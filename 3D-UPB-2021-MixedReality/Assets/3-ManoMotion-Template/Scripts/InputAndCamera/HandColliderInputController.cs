using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HandColliderInputController : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    public float ColliderScale = 0.5f;

    public GameObject physicsObjectPrefab;
    public static Collider CurrentCollider;
    public static List<Collider> InteractableColliders;

    // Start is called before the first frame update
    void Start()
    {
        // Scale collider radius (parent object)
        transform.parent.localScale = new Vector3(ColliderScale, ColliderScale, ColliderScale);
        InteractableColliders = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool LMB_Down = false;
        bool LMB_Up = false;

        HandInfo handInfo = new HandInfo();

        // Desktop version
        if (ARInitManager.CurrentGameMode == ARInitManager.GameMode.Desktop)
        {
            LMB_Down = Input.GetMouseButtonDown(0);
            LMB_Up = Input.GetMouseButtonUp(0);
        }
        // AR version
        else if (ARInitManager.CurrentGameMode == ARInitManager.GameMode.AR)
        {
            // All info for a hand
            handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
        }

        // LMB down (desktop) or GRAB gesture (AR)
        if (LMB_Down == true ||
           (handInfo.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE && ARInitManager.CurrentGameMode == ARInitManager.GameMode.AR))
        {
            if (CurrentCollider != null)
            {
                switch (CurrentCollider.gameObject.layer)
                {
                    case Constants.LAYER_PHYSICS_OBJECT:
                        CurrentCollider.GetComponent<InteractableMovable>().HandleGrab();
                        break;

                    default: break;
                }
            }
        }

        // LMB up (desktop) or RELEASE gesture (AR)
        if (LMB_Up == true ||
           (handInfo.gesture_info.mano_gesture_trigger == ManoGestureTrigger.RELEASE_GESTURE && ARInitManager.CurrentGameMode == ARInitManager.GameMode.AR))
        {
            if (CurrentCollider != null)
            {
                switch (CurrentCollider.gameObject.layer)
                {
                    case Constants.LAYER_PHYSICS_OBJECT:

                        // TODO: Change action from 'HandleRelease()' to 'HandleThrow()'
                        CurrentCollider.GetComponent<InteractableMovable>().HandleRelease();

                        break;

                    default: break;
                }

                // If mouse released and hand collider not touching currently selected collider
                if (InteractableColliders.Contains(CurrentCollider) == false)
                    CurrentCollider.GetComponent<Interactable>().SetOriginalMaterial();

                CurrentCollider = null;

            }
        }

        /* TODO:          Instatiate the physics object using a gesture (use the 'PICK' trigger gesture)
        *  Hints:         'handInfo.tracking_info.poi' will give you the pinch position in normalized screen coordiantes [0...1]. You must convert
        *                 these to actual pixel values by multiplying with Constants.SCREEN_WIDTH / SCREEN_HEIGHT. Use these to perform
        *                 a raycast to the world and if the ray hits the object named 'PlayPlane', then instantiate 'physicsObjectPrefab'
        *  (!) Important: Instantiate the object 'physicsObjectPrefab' as a child of 'ScenePrefab(Clone)'
        */
        // Code here

        // This starts power bar timer if physics object is held in hand
        if (CurrentCollider != null)
        {
            if(CurrentCollider.GetComponent<InteractableMovable>().IsInteractedWith == true)
                // Handle physics object throw power calculation
                CurrentCollider.GetComponent<UpdatePhysicsObjectThrowPower>().StartTimer();
            else
                CurrentCollider.GetComponent<UpdatePhysicsObjectThrowPower>().StopTimer();
        }
    }

    private void FixedUpdate()
    {        
        // If collider is an interactable physics object and is interacted with
        if (CurrentCollider != null &&
            CurrentCollider.GetComponent<Interactable>().IsInteractedWith == true &&
            CurrentCollider.gameObject.layer == Constants.LAYER_PHYSICS_OBJECT)
        {
            // Grab & hold & move gesture for interactables
            CurrentCollider.GetComponent<InteractableMovable>().HandleGrabMove();
        }
        // Else, find which object is best suited to be highlighted
        else
        {
            HandleColliderSelection();
        }

        // Clear interactables
        InteractableColliders.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Constants.LAYER_PHYSICS_OBJECT)
            InteractableColliders.Add(other);
    }

    private void HandleColliderSelection()
    {
        float distToCam = float.MaxValue;
        CurrentCollider = new Collider();

        float pointRayDiffVectorMagn = float.MaxValue;

        // Get closest collider to camera
        foreach (Collider collider in InteractableColliders)
        {
            // Select collider which is closest to the ray shot out from camera throughout the current point on screen
            if (ARInitManager.CurrentHandColliderSelectionMode == ARInitManager.HandColliderSelectionMode.ClosestToRay)
            {
                Vector3 currentPointRayDiffVector = CameraInputController.GetRayPointVectorDifference(collider.transform.position);
                float currentPointRayDiffVectorMagn = currentPointRayDiffVector.sqrMagnitude;

                if (currentPointRayDiffVectorMagn < pointRayDiffVectorMagn)
                {
                    pointRayDiffVectorMagn = currentPointRayDiffVectorMagn;
                    CurrentCollider = collider;
                }
            }

            // Select collider which is closest to the camera
            else if (ARInitManager.CurrentHandColliderSelectionMode == ARInitManager.HandColliderSelectionMode.ClosestToCam)
            {
                // Get collider's distance to main camera
                float currentDistToCam = Vector3.Distance(collider.transform.position, Camera.main.transform.position);

                // If this distance is shorter than the current minimum, save it
                if (currentDistToCam < distToCam)
                {
                    distToCam = currentDistToCam;
                    CurrentCollider = collider;
                }
            }

            // Un-highlight all colliders (before highlighting valid one)
            collider.GetComponent<Interactable>().SetOriginalMaterial();           
        }

        // If collider is valid, highlight it
        if (CurrentCollider != null)
            CurrentCollider.GetComponent<Interactable>().SetHighlightMaterial();
    }
}
