using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    // bool jump = false;
    bool hiding = false;
    public float runSpeed = 40f;
    void Update()
    {
        //if (Input.GetButtonDown(InputAxes.Jump))
        //{
        //    Debug.Log("JUMP PRESSED");
        //    jump = true;
        //}
        //else
        //{
        //    jump = false;
        //}


        if (Input.GetButtonDown(InputAxes.Interact))
        {
            if (!hiding && controller.canHideInf())
            {
                hiding = true;
                controller.currentState.ChangeState(controller.HidingState);
            }
            else
            {
                hiding = false;
                controller.currentState.ChangeState(controller.ExposedState);
            }
            controller.currentState.DoState(hiding);
        }

        if(!hiding)
        {
            if(Input.GetButtonDown(InputAxes.Torch) && controller.playerTorch.usableBool()) 
            {
                controller.playerTorch.SetActive(!controller.playerTorch.torchLight.enabled);
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

            controller.currentState.DoState(Input.GetButtonDown(InputAxes.Jump));
        }
    }
}