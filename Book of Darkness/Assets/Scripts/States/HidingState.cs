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

    public void DoState(bool hide)
    {
        player.HidePlayer(true);
        player.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
    }

    public void ChangeState(PlayerState state)
    {
        player.HidePlayer(false);
        player.currentState = state;
    }

    public string NameToString()
    {
        return "Hidng State";
    }

}
