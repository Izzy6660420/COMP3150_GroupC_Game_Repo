using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingState : PlayerState
{
    Player player;
    public HidingState(Player cont)
    {
        player = cont;
    }

    public void DoState()
    {
        player.HidePlayer(true);
        player.StopMovement();
    }

    public void ChangeState(PlayerState state)
    {
        player.HidePlayer(false);
        player.currentState = state;
    }
}
