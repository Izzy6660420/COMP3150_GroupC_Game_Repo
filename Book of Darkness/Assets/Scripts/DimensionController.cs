using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    static public DimensionController instance;
    static private Camera mainCamera;
    public Camera nightmare;
    public Camera darkness;

    public const string nightmareStr = "nightmare";
    public const string darknessStr = "darkness";
    public string dimensionStr;

    public event Action DimensionSwitchEvent;

    public static DimensionController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        dimensionStr = nightmareStr;
    }

    public void CameraSwitch()
    {
        if (!Inventory.instance.HasItem("Book"))
            return;
        if(nightmare.enabled) 
        {
            nightmare.enabled = false;
            darkness.enabled = true;
            mainCamera = darkness;
            dimensionStr = darknessStr;
        }
        else
        {
            nightmare.enabled = true;
            darkness.enabled = false;
            mainCamera = nightmare;
            dimensionStr = nightmareStr;
        }
        DimensionSwitchEvent?.Invoke();
    }

    public Camera MainCam()
    {
        return mainCamera;
    }
}
