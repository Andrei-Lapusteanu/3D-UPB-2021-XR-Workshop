using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMovable : Interactable
{
    [SerializeField] private float distanceFromCamera = 1.2f;
    private float interactableMoveForce_XY = 500;
    private float interactableMoveForce_Z = 500;

    public void HandleGrab()
    {
        // Set interaction flag
        IsInteractedWith = true;
    }

    public void HandleRelease()
    {
        // Set interaction flag
        IsInteractedWith = false;
    }

    public void HandleThrow()
    {
        // Set interaction flag
        IsInteractedWith = false;

        // Throw action
        GetComponent<HandlePhysicsObjectThrow>().ComputeThrowAction();
    }

    public void HandleGrabMove()
    {
        // Get the difference between the rigidbody's position and the closest point on the camera's ray
        Vector3 rayPointDifference = CameraInputController.GetRayPointVectorDifference(rigidbody.position);

        // Set velocity to rigid body - it will smoothly move towards the closest point on the ray
        rigidbody.velocity = rayPointDifference * interactableMoveForce_XY * Time.deltaTime;

        // Adjust object's position such that it keeps a constant distance from the camera. Otherwise it's chaotic.
        // May need some adjusments, feel free to do whatever
        Vector3 target = Camera.main.transform.position + (CameraInputController.CameraRay.direction * distanceFromCamera);
        rigidbody.velocity -= (rigidbody.position - target) * interactableMoveForce_Z * Time.deltaTime;

        // Get current rotation in angles
        Vector3 objectRotationAngles = rigidbody.transform.eulerAngles;

        // Adjust rotation smoothly by lerping to Quaternion.identity
        rigidbody.gameObject.transform.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.identity, 0.1f);
    }

    public void HandleDestroy(bool skipRelease = false)
    {
        // Release object
        if(skipRelease == true)
            HandleRelease();

        // Disable collider (so that the hand collider won't pick up on it)
        GetComponent<Collider>().enabled = false;
        HandColliderInputController.InteractableColliders.Clear();

        // Now it can be safely destroyed
        Destroy(gameObject);
    }
}
