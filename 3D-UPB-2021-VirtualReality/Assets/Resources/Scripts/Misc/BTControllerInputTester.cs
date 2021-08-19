using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BTControllerInputTester : MonoBehaviour
{
    public Button JoystickButtonA;
    public Button JoystickButtonB;
    public Button JoystickButtonC;
    public Button JoystickButtonD;
    public Button JoystickButtonL1;
    public Button JoystickButtonR1;
    public Button JoystickButtonStart;
    public Button JoystickStick;
    public TextMeshProUGUI JoysticStick_X_Value;
    public TextMeshProUGUI JoysticStick_Y_Value;

    Vector3 initStickPos;

    private void Start()
    {
        initStickPos = JoystickStick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Joystick Button A"))
            JoystickButtonA.image.color = Color.green;
        else
            JoystickButtonA.image.color = Color.white;

        if (Input.GetButton("Joystick Button B"))
            JoystickButtonB.image.color = Color.green;
        else
            JoystickButtonB.image.color = Color.white;

        if (Input.GetButton("Joystick Button C"))
            JoystickButtonC.image.color = Color.green;
        else
            JoystickButtonC.image.color = Color.white;

        if (Input.GetButton("Joystick Button D"))
            JoystickButtonD.image.color = Color.green;
        else
            JoystickButtonD.image.color = Color.white;

        if (Input.GetButton("Joystick Button Start"))
            JoystickButtonStart.image.color = Color.green;
        else
            JoystickButtonStart.image.color = Color.white;

        // TODO: Setup buttons R1 and L1 in code

        float stick_x_axis = Input.GetAxis("Horizontal");
        float stick_y_axis = Input.GetAxis("Vertical");

        JoysticStick_X_Value.text = "X-axis: " + stick_x_axis.ToString("0.000");
        JoysticStick_Y_Value.text = "Y-axis: " + stick_y_axis.ToString("0.000");

        JoystickStick.transform.position = new Vector3(
            initStickPos.x + (stick_x_axis * 100),
            initStickPos.y + (stick_y_axis * 100),
            0f);
    }
}
