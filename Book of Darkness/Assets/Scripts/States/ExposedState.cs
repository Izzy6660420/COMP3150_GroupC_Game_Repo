using UnityEngine;

public class ExposedState : PlayerState
{
    Player player;
    float hMove = 0f;
    public float runSpeed = 30f;

    public ExposedState(Player cont)
    {
        player = cont;
    }

    public void DoState()
    {
        hMove = Input.GetAxisRaw(InputAxes.Horizontal) * runSpeed;
        player.Move(hMove * Time.fixedDeltaTime);
    }

    public void ChangeState(PlayerState state)
    {
        player.currentState = state;
    }
}
