using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposedState : PlayerState
{
    Player player;
    float hMove = 0f;
    public float runSpeed = 40f;
    public ExposedState(Player cont)
    {
        player = cont;
    }

    public void DoState(bool jump)
    {
        hMove = Input.GetAxisRaw(InputAxes.Horizontal) * runSpeed;
        player.Move(hMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void ChangeState(PlayerState state)
    {
        player.currentState = state;
    }

    public string NameToString()
    {
        return "Exposed State";
    }
}
