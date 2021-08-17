using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ManoDebugScript : MonoBehaviour
{
    private bool isSessionQualityOK;

    public TextMeshProUGUI debug1;
    public TextMeshProUGUI debug2;
    public TextMeshProUGUI debug3;
    public TextMeshProUGUI debug4;
    public TextMeshProUGUI debug5;
    public TextMeshProUGUI debug6;

    // Start is called before the first frame update
    void Start()
    {
        ARSession.stateChanged += HandleStateChanged;
    }


    // Update is called once per frame
    void Update()
    {
        DemoFunction();
    }

    private void HandleStateChanged(ARSessionStateChangedEventArgs stateEventArguments)
    {
        isSessionQualityOK = stateEventArguments.state == ARSessionState.SessionTracking;
    }

    private void DemoFunction()
    {
        // All info for a hand
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;

        // All gesture information regarding the hand
        GestureInfo gestureInfo = handInfo.gesture_info;

        /* Using the previously attained hand & gesture infos,
        let's get relevant data (which will be displayed on-screen) */
        ManoGestureTrigger gestTrigger = gestureInfo.mano_gesture_trigger;          // Trigger gesture
        ManoClass gestClass = gestureInfo.mano_class;                               // Gesture class
        HandSide gestHandSide = gestureInfo.hand_side;                              // Hand side
        ManoGestureContinuous gestContinuous = gestureInfo.mano_gesture_continuous; // Continuous gesture
        int gestState = gestureInfo.state;                                          // Gesture state
        Vector3 palmPos = handInfo.tracking_info.palm_center;                       // Screen coords [0...1]

        // Multiply screen coords with width and height of your display
        palmPos.x *= Constants.SCREEN_WIDTH;
        palmPos.y *= Constants.SCREEN_HEIGHT;

        /* Debug 1 - ManoClass */
        // ManoClass.NO_HAND
        // ManoClass.GRAB_GESTURE
        // ManoClass.POINTER_GESTURE
        // ManoClass.PINCH_GESTURE

        /* Debug 2 - HandSide */
        // HandSide.None;
        // HandSide.Backside;
        // HandSide.Palmside;

        /* Debug 3 - ManoGestureContinuous */
        // ManoGestureContinuous.NO_GESTURE
        // ManoGestureContinuous.HOLD_GESTURE
        // ManoGestureContinuous.OPEN_HAND_GESTURE
        // ManoGestureContinuous.OPEN_PINCH_GESTURE
        // ManoGestureContinuous.CLOSED_HAND_GESTURE
        // ManoGestureContinuous.POINTER_GESTURE

        /* Debug 4 - ManoGestureTrigger */
        // ManoGestureTrigger.NO_GESTURE
        // ManoGestureTrigger.CLICK
        // ManoGestureTrigger.RELEASE_GESTURE
        // ManoGestureTrigger.GRAB_GESTURE
        // ManoGestureTrigger.PICK
        // ManoGestureTrigger.DROP

        debug1.text = gestClass.ToString();
        debug2.text = gestHandSide.ToString();
        debug3.text = gestContinuous.ToString();
        debug4.text = gestTrigger.ToString();
        debug5.text = gestState.ToString();
        debug6.text = palmPos.ToString();
    }
}
