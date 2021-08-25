using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    static private DimensionController instance;
    static private Camera MainCamera;
    public Camera nightmare;
    public Camera darkness;


    public static DimensionController Instance
    {
        get
        {
            return instance;
        }
    }

    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CameraSwitch()
    {
        if(nightmare.enabled) 
        {
            nightmare.enabled = false;
            darkness.enabled = true;
            MainCamera = darkness;
        }
        else
        {
            nightmare.enabled = true;
            darkness.enabled = false;
            MainCamera = nightmare;
        }
    }

    public Camera MainCam()
    {
        return MainCamera;
    }
}
