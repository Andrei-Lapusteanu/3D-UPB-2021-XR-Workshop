using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSceneBuilder : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject SceneToBuildPrefab;
    public ARInitManager arInitManager;

    private bool isSessionQualityOK;
    private bool sceneInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        ARSession.stateChanged += HandleStateChanged;
        sceneInstantiated = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isSessionQualityOK && sceneInstantiated == false)
            SpawnScene();
    }

    private void HandleStateChanged(ARSessionStateChangedEventArgs stateEventArguments)
    {
        isSessionQualityOK = stateEventArguments.state == ARSessionState.SessionTracking;
    }

    // Reset button (phase 2)
    public void ResetSceneInstantiation()
    {
        sceneInstantiated = false;

        try
        {
            Destroy(GameObject.Find("ScenePrefab(Clone)").gameObject);
        }
        catch (Exception) { }
    }

    // Confirm button action (phase 2)
    public void ConfirmSceneInstantiation()
    {
        // Advance from phase 2 to phase 3
        ARGameSetupController.CurrentGamePhase = Constants.GameInitPhase.SceneAdjustments;

        // Hide detected plane(s)
        arInitManager.HideDetectedPlanes();

        // Disable script
        this.gameObject.GetComponent<ARSceneBuilder>().enabled = false;
    }

    private void SpawnScene()
    {
        // All info for a hand
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;

        // All gesture information regarding the hand
        GestureInfo gestureInfo = handInfo.gesture_info;

        // Get the detected gesture in the frame
        ManoGestureTrigger currentDetectedTriggerGesture = gestureInfo.mano_gesture_trigger;

        // Check gesture type (only is phase 2 active!)
        if (currentDetectedTriggerGesture == ManoGestureTrigger.GRAB_GESTURE &&
            ARGameSetupController.CurrentGamePhase == Constants.GameInitPhase.ScenePlacement)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(handInfo.tracking_info.palm_center.x * Constants.SCREEN_WIDTH, handInfo.tracking_info.palm_center.y * Constants.SCREEN_HEIGHT, 0));

            // Perform raycast
            if (arRaycastManager.Raycast(ray, hits))
            {
                foreach (ARRaycastHit hit in hits)
                {
                    // Instantiate scene on plane at raycast hit position
                    GameObject scenePrefab = Instantiate(SceneToBuildPrefab, hit.pose.position, Quaternion.identity, GameObject.Find("AR-Root").transform);

                    // Only one instance, we test this with sceneInstantiated
                    sceneInstantiated = true;

                    // break, in case multiple hits on the plane are registered
                    break;
                }
            }

            // Haptic feedback
            Handheld.Vibrate();
        }
    }
}
