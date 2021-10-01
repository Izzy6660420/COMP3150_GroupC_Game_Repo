using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Player player;
    public Transform arm;
    public Light2D torchLight;
    public Light2D torchLightBG;
    public PolygonCollider2D polyCol;
    public GameObject gameObj;

    public float power = 20.0f;
    private float maxPower = 20.0f;
    private float minPower = 0.0f;
    public float powerDrain = 1.0f;
    public AudioClip buttonClickSfx;

    private TorchUI torchBar;
    private bool usable = true;

    void Start()
    {
        torchBar = TorchUI.instance;
        torchBar.SetBatteryCeiling(maxPower, minPower);
        player = Player.instance;
    }

    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mPos - (Vector2)arm.position).normalized;

        if (player.facingRight)
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
        AudioManager.instance.PlaySound(buttonClickSfx, transform.position);
    }

    public bool usableBool()
    {
        return usable;
    }
}
