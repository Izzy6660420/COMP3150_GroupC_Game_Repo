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
            }

            controller.currentState.DoState(Input.GetButtonDown(InputAxes.Jump));
        }
    }
}