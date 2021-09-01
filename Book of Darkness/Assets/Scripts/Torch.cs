using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public Transform arm;
    public CharacterController2D cc2D;
    public float power = 20.0f;
    private float maxPower = 20.0f;
    private float minPower = 0.0f;
    public float powerDrain = 1.0f;
    private bool usable = true;
    public Light2D torchLight;
    public Light2D torchLightBG;
    public PolygonCollider2D polyCol;
    public TorchUI torchBar;
    public GameObject gameObj;

    void Start()
    {
        torchBar.SetBatteryCeiling(maxPower, minPower);
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

        if (torchLight.enabled)
        {
            power -= Time.deltaTime * powerDrain;
            torchBar.SetBattery(power);
        }

        if (power > maxPower)
        {
            power = maxPower;
        }

        if (power < minPower)
        {
            power = minPower;
            SetActive(false);
            usable = false;
        }

        // if (Input.GetButtonDown(InputAxes.Torch) && usable)
        // {
        //     SetActive(!torchLight.enabled);
        // }

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

    public void SetActive(bool b)
    {
        torchLight.enabled = b;
        torchLightBG.enabled = b;
        polyCol.enabled = b;
        gameObj.SetActive(b);
    }

    public bool usableBool()
    {
        return usable;
    }
}
