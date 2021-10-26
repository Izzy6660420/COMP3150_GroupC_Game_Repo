using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PanicMeterEffect : MonoBehaviour
{

    Volume volume;
    public DimensionController dimC;    
    Vignette vig;
    ChromaticAberration ca;

    // Start is called before the first frame update
    void Start()
    {

        volume = gameObject.GetComponent<Volume>();

        if(volume.profile.TryGet<Vignette>(out vig))
        {
            vig.intensity.value = 0;
        }

        if(volume.profile.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value = 0;
        }
    }

    void Update()
    {
        vig.intensity.value = dimC.currentPanic() / dimC.maxPanicVal();
        ca.intensity.value = dimC.currentPanic() / dimC.maxPanicVal();
    }

    void FixedUpdate()
    {

    }
}
