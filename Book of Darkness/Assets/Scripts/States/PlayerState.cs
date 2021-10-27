using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    void DoState();
    void ChangeState(PlayerState state);
}
