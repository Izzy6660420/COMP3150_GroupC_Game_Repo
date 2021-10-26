using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    bool hiding = false;

    void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        if (Input.GetButtonDown(InputAxes.Interact))
        {
            if (!hiding && player.canHideInf())
            {
                hiding = true;
                player.currentState.ChangeState(player.HidingState);
            }
            else
            {
                hiding = false;
                player.currentState.ChangeState(player.ExposedState);
            }
            player.currentState.DoState(hiding);
        }

        if(!hiding)
        {
            // Torch Control
            if(Input.GetButtonDown(InputAxes.Torch) && player.torch.usableBool()) 
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                player.torch.SetActive(!player.torch.torchLight.enabled);
            }

            if(Input.GetButtonDown(InputAxes.DimensionSwitch))
            {

                DimensionController.Instance.CameraSwitch();

                if(DimensionController.Instance.dimensionInf().Equals("darkness")) 
                {
                    Physics2D.IgnoreLayerCollision(3, 9, false);
                    Physics2D.IgnoreLayerCollision(3, 8, true);
                } 
                else if(DimensionController.Instance.dimensionInf().Equals("nightmare"))
                {
                    Physics2D.IgnoreLayerCollision(3, 9, true);
                    Physics2D.IgnoreLayerCollision(3, 8, false);
                }
            }

            player.currentState.DoState(Input.GetButtonDown(InputAxes.Jump));
        }
    }
}