using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    static private DimensionController instance;
    static private Camera mainCamera;
    public Camera nightmare;
    public Camera darkness;

    public PanicUI panicBar;
    private float panic = 0.0f;
    private float maxPanic = 10.0f;
    private float minPanic = 0.0f;
    public float panicGain = 1.0f;

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
        panicBar.SetPanicCeiling(maxPanic, minPanic);
    }

    void Update()
    {
        if (darkness.enabled)
        {
            panic += Time.deltaTime * panicGain;
            panicBar.SetPanic(panic);
        }
    }

    public void CameraSwitch()
    {
        if(nightmare.enabled) 
        {
            nightmare.enabled = false;
            darkness.enabled = true;
            mainCamera = darkness;
        }
        else
        {
            nightmare.enabled = true;
            darkness.enabled = false;
            mainCamera = nightmare;
        }
    }

    public Camera MainCam()
    {
        return mainCamera;
    }
}
