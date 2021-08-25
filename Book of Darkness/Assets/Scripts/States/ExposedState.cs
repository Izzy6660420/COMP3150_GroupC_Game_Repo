using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposedState : PlayerState
{
    CharacterController2D controller;
    float hMove = 0f;
    public float runSpeed = 40f;
    public ExposedState(CharacterController2D cont)
    {
        controller = cont;
    }

    public void DoState(bool jump)
    {
        hMove = Input.GetAxisRaw(InputAxes.Horizontal) * runSpeed;
        controller.Move(hMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void ChangeState(PlayerState state)
    {
        controller.currentState = state;
    }


}
