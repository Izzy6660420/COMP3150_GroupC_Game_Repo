using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public Transform arm;
    public CharacterController2D cc2D;

    void Start()
    {
        
    }

    void Update()
    {   
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mPos - (Vector2)arm.position).normalized;

        if (cc2D.m_FacingRight)
        {
            arm.right = dir;
        }
        else
        {
            arm.right = -dir;
        }


        // Rotate angle restriction code block
        //
        // Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mPos.z = transform.position.z;
        // Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, mPos - transform.position);
        // Vector3 nAngles = targetRotation.eulerAngles;
        // nAngles.z = nAngles.z < 180? Mathf.Clamp(nAngles.z, 60, 120) : Mathf.Clamp(nAngles.z, 240, 300);
        // targetRotation.eulerAngles = nAngles;
        // Debug.Log("nAngles: " + nAngles);
        // transform.rotation = targetRotation;
    }
}
