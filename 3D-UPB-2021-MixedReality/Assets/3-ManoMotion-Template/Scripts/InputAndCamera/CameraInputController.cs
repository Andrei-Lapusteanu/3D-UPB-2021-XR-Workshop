using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputController : MonoBehaviour
{
    public GameObject handColliderPrefab;
    public float handColliderRadius = 0.5f;

    private GameObject handColliderInst;
    private Transform handColliderTransf;

    public static Ray CameraRay;

    // Start is called before the first frame update
    void Start()
    {
        handColliderInst = Instantiate(handColliderPrefab, Camera.main.transform);
        handColliderTransf = handColliderInst.transform.Find("HandCollider").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Desktop version
        if (ARInitManager.CurrentGameMode == ARInitManager.GameMode.Desktop)
        {
            Vector3 mousePos = Input.mousePosition;
            UpdateHandCollider(mousePos);
        }
        // AR version
        else if (ARInitManager.CurrentGameMode == ARInitManager.GameMode.AR)
        {
            HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
            UpdateHandCollider(new Vector3(handInfo.tracking_info.palm_center.x * Constants.SCREEN_WIDTH, handInfo.tracking_info.palm_center.y * Constants.SCREEN_HEIGHT, 0));
        }
    }

    private void UpdateHandCollider(Vector3 screenPointPos)
    {
        // Create a ray shooting out from the camera towards the world (in Desktop Mode, mouse pos, in AR mode, palm center pos)
        CameraRay = Camera.main.ScreenPointToRay(screenPointPos);

        // Create quaternion for rotating hand collider
        Quaternion handColliderQuat = new Quaternion();

        // Set quaternion rotation dependent on the ray's direction & apply rotation
        handColliderQuat.SetLookRotation(CameraRay.direction, new Vector3(0, 1, 0));
        handColliderInst.transform.rotation = handColliderQuat;

        // Set radius of hand collider (cylinder)
        handColliderTransf.localScale = new Vector3(handColliderRadius, handColliderTransf.localScale.y, handColliderRadius);

        // Draw ray
        Debug.DrawRay(Camera.main.transform.position, CameraRay.direction * 100, Color.green);
    }

    public static Vector3 GetClosestPointOnRay(Vector3 referencePoint)
    {
        return CameraRay.origin + CameraRay.direction * Vector3.Dot(CameraRay.direction, referencePoint - CameraRay.origin);
    }

    public static Vector3 GetRayPointVectorDifference(Vector3 referencePoint)
    {
        return (CameraRay.origin + CameraRay.direction * Vector3.Dot(CameraRay.direction, referencePoint - CameraRay.origin)) - referencePoint;
    }
}