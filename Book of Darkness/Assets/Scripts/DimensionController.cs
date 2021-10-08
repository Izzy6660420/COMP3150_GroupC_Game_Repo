using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    static public DimensionController instance;
    static private Camera mainCamera;
    public Camera nightmare;
    public Camera darkness;
    public GameObject globalLight;

    private PanicUI panicBar;
    private float panic = 0.0f;
    private float maxPanic = 10.0f;
    private float minPanic = 0.0f;
    public float panicGain = 1.0f;

    public const string nightmareStr = "nightmare";
    public const string darknessStr = "darkness";
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
        if (!Inventory.instance.HasItem("Book"))
            return;
        if(nightmare.enabled) 
        {
            nightmare.enabled = false;
            darkness.enabled = true;
            globalLight.SetActive(true);
            mainCamera = darkness;
            dimensionStr = darknessStr;
        }
        else
        {
            nightmare.enabled = true;
            darkness.enabled = false;
            globalLight.SetActive(false);
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
