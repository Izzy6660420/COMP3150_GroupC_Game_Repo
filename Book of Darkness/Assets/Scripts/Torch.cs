using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public Transform arm;
    public CharacterController2D cc2D;
    public Light2D torchLight;
    public Light2D torchLightBG;
    public PolygonCollider2D polyCol;
    public GameObject gameObj;

    public float power = 20.0f;
    private float maxPower = 20.0f;
    private float minPower = 0.0f;
    public float powerDrain = 1.0f;

    private TorchUI torchBar;
    private bool usable = true;

    void Start()
    {
        torchBar = TorchUI.instance;
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
    }

    public void SetActive(bool b)
    {
        // Item check for torch in inventory
        //if (!Inventory.instance.hasItem("Torch"))
        //    return;

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
