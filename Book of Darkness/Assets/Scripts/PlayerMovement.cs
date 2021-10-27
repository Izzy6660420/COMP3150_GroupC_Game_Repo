using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    DimensionController dim;
    bool hiding = false;

    void Start()
    {
        player = Player.instance;
        dim = DimensionController.instance;
    }

    void Update()
    {
        if (Input.GetButtonDown(InputAxes.Interact))
        {
            if (!hiding && player.canHide)
            {
                hiding = true;
                player.currentState.ChangeState(player.HidingState);
                PlayerTorch.instance.SetActive(false);
            }
            else
            {
                hiding = false;
                player.currentState.ChangeState(player.ExposedState);
            }
            player.currentState.DoState();
        }

        if(!hiding)
        {
            if(Input.GetButtonDown(InputAxes.DimensionSwitch))
            {
                dim.CameraSwitch();

                if(dim.dimensionStr.Equals("darkness")) 
                {
                    Physics2D.IgnoreLayerCollision(3, 9, false);
                    Physics2D.IgnoreLayerCollision(3, 8, true);
                } 
                else if(dim.dimensionStr.Equals("nightmare"))
                {
                    Physics2D.IgnoreLayerCollision(3, 9, true);
                    Physics2D.IgnoreLayerCollision(3, 8, false);
                }
            }
            player.currentState.DoState();
        }
    }
}