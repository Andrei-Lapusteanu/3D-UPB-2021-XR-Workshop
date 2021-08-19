using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    private const float DEFAULT_MOVE_SPEED = 8.0f;
    private const float SPRINT_VELOCITY_BOOST = 4.0f;
    private const float GRAVITY_FORCE = -3f;          // Ochiometric
    private const float JUMP_JUMP_FORCE = 0.45f;

    private float currentMoveSpeed = DEFAULT_MOVE_SPEED;
    private CharacterController characterController;
    private Vector3 movementVector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If character is touching the ground
        if (characterController.isGrounded)
        {
            // Stick the character to the floor (always grounded)
            if (movementVector.y <= 0)
                movementVector.y = -0.1f;

            // Keyboard (-1/0/1) or stick (-1...1) input
            float axis_x = Input.GetAxis("Horizontal");
            float axis_y = Input.GetAxis("Vertical");

            // Compute movement vector, based of player's orientation
            // movementVector = (axis_y * transform.forward) + (axis_x * transform.right);
            movementVector = new Vector3(axis_x, 0f, axis_y);
            movementVector = Camera.main.transform.TransformDirection(movementVector);

            // Jump (Space)
            if (Input.GetKey(KeyCode.Space) || Input.GetButton("Joystick Button A"))
                movementVector.y += Mathf.Sqrt(JUMP_JUMP_FORCE * GRAVITY_FORCE * -1);

            // Sprint (LShift)
            if (Input.GetKey(KeyCode.LeftShift))
                currentMoveSpeed = DEFAULT_MOVE_SPEED + SPRINT_VELOCITY_BOOST;
            else 
                currentMoveSpeed = DEFAULT_MOVE_SPEED;
        }

        // Apply gravity
        movementVector.y += GRAVITY_FORCE * Time.deltaTime;

        // Apply movement to character controller
        characterController.Move(movementVector * currentMoveSpeed * Time.deltaTime);
    }
}
