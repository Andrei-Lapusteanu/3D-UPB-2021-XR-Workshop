using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInitManager : MonoBehaviour
{
    public enum GameMode 
    { 
        Desktop, 
        AR 
    };

    public enum HandColliderSelectionMode 
    { 
        ClosestToRay, 
        ClosestToCam
    }

    // Vars to be viewed inside Unity's inspector
    public GameMode GlobalGameMode = GameMode.AR;
    public HandColliderSelectionMode GlobalHandColliderSelectionMode = HandColliderSelectionMode.ClosestToRay;

    // Static vars cannot be viewed inside Unity's inspector
    public static GameMode CurrentGameMode;
    public static HandColliderSelectionMode CurrentHandColliderSelectionMode = HandColliderSelectionMode.ClosestToRay;

    public ARPlaneManager planeManager;
    public ARSession m_session;


    // Update is called once per frame
    void Update()
    {
        CurrentGameMode = GlobalGameMode;
        CurrentHandColliderSelectionMode = GlobalHandColliderSelectionMode;
    }

    public void DisablePlaneDetection()
    {
        planeManager.requestedDetectionMode = PlaneDetectionMode.None;
    }

    public void EnablePlaneDetection()
    {
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
    }

    public void HideDetectedPlanes()
    {
        foreach (ARPlane plane in planeManager.trackables)
            plane.gameObject.SetActive(false);
    }

    // Reset button action (phase 1)
    public void ResetPlaneDetection()
    {
        HideDetectedPlanes();

        // Restart the AR session
        m_session.Reset();
    }

    // Confirm button action (phase 1)
    public void ConfirmPlaneDetection()
    {
        DisablePlaneDetection();

        // Advance from phase 1 to phase 2
        ARGameSetupController.CurrentGamePhase = Constants.GameInitPhase.ScenePlacement;
    }
}

