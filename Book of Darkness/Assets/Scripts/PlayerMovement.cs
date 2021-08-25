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
        // if (Input.GetButtonDown(InputAxes.Jump))
        // {
        //     Debug.Log("JUMP PRESSED");
        //     jump = true;
        // }
        // else
        // {
        //     jump = false;
        // }

        if (controller.canHideInf())
        {
            if (Input.GetButtonDown(InputAxes.Interact))
            {
                if (!hiding)
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
        }
        

        if(!hiding)
        {
            if(Input.GetButtonDown(InputAxes.Torch) && controller.m_playerTorch.usableBool()) 
            {
                controller.m_playerTorch.SetActive(!controller.m_playerTorch.torchLight.enabled);
            }

            controller.currentState.DoState(Input.GetButtonDown(InputAxes.Jump));
        }
    }

    void FixedUpdate()
    {

    }
}