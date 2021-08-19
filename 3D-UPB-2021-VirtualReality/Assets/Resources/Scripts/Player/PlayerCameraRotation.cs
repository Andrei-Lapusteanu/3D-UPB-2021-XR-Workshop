using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{
    private const float ROT_VERT_MIN_ANGLE = -90f;
    private const float ROT_VERT_MAX_ANGLE =  90f;

    private CharacterController characterController;
    private float yaw = 0f;                              // Rotation around the Y axis
    private float pitch = 0f;                            // Rotation around the X axis
    [SerializeField] private float sensitivityX = 2.5f;
    [SerializeField] private float sensitivityY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update angles from mouse input
        yaw += sensitivityX * Input.GetAxis("Mouse X");
        pitch -= sensitivityY * Input.GetAxis("Mouse Y");

        // Limit vertical rotation
        pitch = Mathf.Clamp(pitch, ROT_VERT_MIN_ANGLE, ROT_VERT_MAX_ANGLE);

        // Apply rotation to character (around Y-axis)
        characterController.transform.eulerAngles = new Vector3(0f, yaw, 0f);

        // Apply rotation to main camera (around X,Y-axis);
        Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
