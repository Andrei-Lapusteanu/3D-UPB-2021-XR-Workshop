using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ManoSimpleScript : MonoBehaviour
{
    private bool isSessionQualityOK;

    void Start()
    {
        ARSession.stateChanged += HandleStateChanged;
    }

    void Update()
    {
        if (isSessionQualityOK)
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

        // All tracking information regarding the hand
        TrackingInfo trackingInfo = handInfo.tracking_info;

        // All gesture information regarding the hand
        GestureInfo gestureInfo = handInfo.gesture_info;

        // Next we test trackingInfo/gestureInfo in order to implement our logic
        // ...

        // ManoClass detected in current frame
        ManoClass manoClass = gestureInfo.mano_class;

        ManoClass classNohand         = ManoClass.NO_HAND;
        ManoClass classGrabGesture    = ManoClass.GRAB_GESTURE;
        ManoClass classPointerGesture = ManoClass.POINTER_GESTURE;
        ManoClass classPinchGesture   = ManoClass.PINCH_GESTURE;

        // HandSide detected in current frame
        HandSide handSide = gestureInfo.hand_side;

        HandSide palmSide = HandSide.Palmside;
        HandSide backSide = HandSide.Backside;

        // Continuous gesture detected in current frame
        ManoGestureContinuous continuousGesture = gestureInfo.mano_gesture_continuous;

        //ManoGestureContinuous noGesture         = ManoGestureContinuous.NO_GESTURE;
        //ManoGestureContinuous holdGesture       = ManoGestureContinuous.HOLD_GESTURE;
        //ManoGestureContinuous openHandGesture   = ManoGestureContinuous.OPEN_HAND_GESTURE;
        //ManoGestureContinuous openPinchGesture  = ManoGestureContinuous.OPEN_PINCH_GESTURE;
        //ManoGestureContinuous closedHandGesture = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        //ManoGestureContinuous pointerGesture    = ManoGestureContinuous.POINTER_GESTURE;


        /* Debug 3 - ManoGestureContinuous */
        // ManoGestureContinuous.NO_GESTURE
        // ManoGestureContinuous.HOLD_GESTURE
        // ManoGestureContinuous.OPEN_HAND_GESTURE
        // ManoGestureContinuous.OPEN_PINCH_GESTURE
        // ManoGestureContinuous.CLOSED_HAND_GESTURE
        // ManoGestureContinuous.POINTER_GESTURE

        // Trigger gesture detected in current frame
        ManoGestureTrigger triggerGesture = gestureInfo.mano_gesture_trigger;

        ManoGestureTrigger noGesture      = ManoGestureTrigger.NO_GESTURE;
        ManoGestureTrigger clickGesture   = ManoGestureTrigger.CLICK;
        ManoGestureTrigger releaseGesture = ManoGestureTrigger.RELEASE_GESTURE;
        ManoGestureTrigger grabGesture    = ManoGestureTrigger.GRAB_GESTURE;
        ManoGestureTrigger pickGesture    = ManoGestureTrigger.PICK;
        ManoGestureTrigger dropGesture    = ManoGestureTrigger.DROP;


        /* Debug 4 - ManoGestureTrigger */
        // ManoGestureTrigger.NO_GESTURE
        // ManoGestureTrigger.CLICK
        // ManoGestureTrigger.RELEASE_GESTURE
        // ManoGestureTrigger.GRAB_GESTURE
        // ManoGestureTrigger.PICK
        // ManoGestureTrigger.DROP

        // Gesture state detected in current frame
        int gestureState = gestureInfo.state;

        // Palm position detected in current frame
        Vector3 palmPosition = trackingInfo.palm_center;  // 0-1 range
        palmPosition.x *= Constants.SCREEN_WIDTH;  // Phone resolution - width  (landscape mode)
        palmPosition.y *= Constants.SCREEN_HEIGHT; // Phone resolution - height (landscape mode)




        //if (gestureState || dropGesture || pickGesture || grabGesture || releaseGesture || clickGesture || triggerGesture || continuousGesture || noGesture || pointerGesture || closedHandGesture || openPinchGesture || openHandGesture || holdGesture || handSide || palmSide || backSide || manoClass || classNohand || classGrabGesture || classPinchGesture || classPointerGesture)
        //{
        //
        //}
        /* Debug 1 - ManoClass */
        // ManoClass.NO_HAND
        // ManoClass.GRAB_GESTURE
        // ManoClass.POINTER_GESTURE
        // ManoClass.PINCH_GESTURE
    }
}
