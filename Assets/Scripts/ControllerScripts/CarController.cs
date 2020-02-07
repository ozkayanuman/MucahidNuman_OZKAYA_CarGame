using UnityEngine;
using System.Collections;

/// <summary>
/// Chose control type (pc or mobile)
/// </summary>
public enum controlMode
{
    KeyBoard, Touch
}

public class CarController : MonoBehaviour
{

    public controlMode CarControlMode;

    public float MaxSpeed = 5.0f;
    public float MaxSteer = 2.0f;


    [SerializeField]
    private float Acceleration = 5.0f;
    [SerializeField]
    private float Steer = 2.0f;

    private bool AccelFwd;
    private bool TouchAccel;
    private bool SteerLeft, SteerRight;
    private bool Pressed;
    private bool byPass;



    void FixedUpdate()
    {
        if (!byPass)
        {

            if (CarControlMode == controlMode.KeyBoard)
            {
                if (Input.anyKey)
                {
                    Pressed = true;
                }
                if (Pressed)
                    Accel(1);                   //Accelerate in forward direction (Now it's fixed)

            }

            if (CarControlMode == controlMode.Touch)
            {
                if (Input.touchCount >= 1)
                {
                    Pressed = true;
                }
                if (Pressed)
                    Accel(1);

            }
        }
    }


    /// <summary>
    /// Functions to be called from Onscreen buttons for touch input.
    /// </summary>
    /// <param name="TouchState"></param>
    public void SetSteerLeft(bool TouchState)
    {
        SteerLeft = TouchState;
    }

    public void SetSteerRight(bool TouchState)
    {
        SteerRight = TouchState;
    }

    //Now Accel. is fixed. May be after we can give it some ability.
    /// <summary>
    /// Gives a specified constant speed for cars and also give a constant speed when inputs are pressed. 
    /// </summary>
    /// <param name="Direction"></param>
    public void Accel(int Direction)
    {

        if (Direction == 1)
        {
            AccelFwd = true;
            if (Acceleration <= MaxSpeed)
            {
                Acceleration = MaxSpeed; // I fixed acceleration and car speed ( it can be edited)
            }

            if (CarControlMode == controlMode.KeyBoard)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    transform.Rotate(Vector3.forward * Steer);          //Steer left
                if (Input.GetKey(KeyCode.RightArrow))
                    transform.Rotate(Vector3.back * Steer);             //Steer right
            }

            else if (CarControlMode == controlMode.Touch)
            {
                if (SteerLeft)
                    transform.Rotate(Vector3.forward * Steer);          //Steer left

                else if (SteerRight)
                    transform.Rotate(Vector3.back * Steer);             ////Steer right
            }
        }

        //Provides the ability to move the vehicle
        if (CarControlMode == controlMode.Touch)
            transform.Translate(Vector2.up * Acceleration * Time.deltaTime); 

        else if (CarControlMode == controlMode.KeyBoard)
            transform.Translate(Vector2.up * Acceleration * Time.deltaTime);
    }


    // Controls when the car will move
    public void DisablePlayerMovement()
    {
        byPass = true;
    }
    public void EnablePlayerMovement()
    {
        byPass = false;
    }


}
