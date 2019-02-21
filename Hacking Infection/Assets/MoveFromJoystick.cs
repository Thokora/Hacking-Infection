using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromJoystick : MonoBehaviour
{
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script
   
  
    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    private Vector3 rightJoystickInput; // hold the input of the Right Joystick

    void FixedUpdate()
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02


        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {

            if (xMovementLeftJoystick > 0)
            {
            }
            if (xMovementLeftJoystick < 0)
            {
                Debug.Log("SOY X MAYOR A 0");
            }

        }

    }
}