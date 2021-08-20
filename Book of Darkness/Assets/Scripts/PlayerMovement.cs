using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float hMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Update()
    {
        hMove = Input.GetAxisRaw(InputAxes.Horizontal) * runSpeed;
        if (Input.GetButtonDown(InputAxes.Jump))
        {
            Debug.Log("JUMP PRESSED");
            jump = true;
        }

        if (Input.GetButtonDown(InputAxes.Crouch))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp(InputAxes.Crouch))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        if (!controller.hiding)
        {
            controller.Move(hMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }
}