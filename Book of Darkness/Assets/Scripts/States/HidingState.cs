using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingState : PlayerState
{

    CharacterController2D controller;
    public HidingState(CharacterController2D cont)
    {
        controller = cont;
    }

    public void DoState(bool hide)
    {
        controller.HidePlayer(true);
    }

    public void ChangeState(PlayerState state)
    {
        controller.HidePlayer(false);
        controller.currentState = state;
    }

}
