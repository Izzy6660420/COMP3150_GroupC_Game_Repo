using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    static private DimensionController instance;
    static private Camera mainCamera;
    public Camera nightmare;
    public Camera darkness;

    private PanicUI panicBar;
    private float panic = 0.0f;
    private float maxPanic = 10.0f;
    private float minPanic = 0.0f;
    public float panicGain = 1.0f;

    private const string nightmareStr = "nightmare";
    private const string darknessStr = "darkness";
    private string dimensionStr;

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
        panicBar = PanicUI.instance;
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
            dimensionStr = darknessStr;
        }
        else
        {
            nightmare.enabled = true;
            darkness.enabled = false;
            mainCamera = nightmare;
            dimensionStr = nightmareStr;
        }
    }

    public Camera MainCam()
    {
        return mainCamera;
    }

    public string dimensionInf()
    {
        return dimensionStr;
    }
}
