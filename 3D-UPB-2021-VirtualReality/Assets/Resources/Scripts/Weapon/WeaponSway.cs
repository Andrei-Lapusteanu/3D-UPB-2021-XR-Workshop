using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float swayAmount;
    [SerializeField] private float maxSwayAmount;
    [SerializeField] private float smoothAmount;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Compute sway movement amount
        float movementX = Input.GetAxis("Mouse X") * swayAmount;
        float movementY = Input.GetAxis("Mouse Y") * swayAmount;

        // Clamp sway
        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

        // Compute final position for prop
        Vector3 finalPosition = new Vector3(-movementX, -movementY, 0f);

        // Modify local position of prop (lerping)
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
    }
}
