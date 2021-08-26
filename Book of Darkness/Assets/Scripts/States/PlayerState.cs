using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    // Start is called before the first frame update
    void DoState(bool stateInform);
    void ChangeState(PlayerState state);
    string NameToString();
}
