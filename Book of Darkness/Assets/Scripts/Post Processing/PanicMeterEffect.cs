using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PanicMeterEffect : MonoBehaviour
{
    Volume volume;
    Vignette vig;
    ChromaticAberration ca;
    PlayerPanic panic;

    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        if(volume.profile.TryGet<Vignette>(out vig)) vig.intensity.value = 0;
        if(volume.profile.TryGet<ChromaticAberration>(out ca)) ca.intensity.value = 0;

        panic = Player.instance.gameObject.GetComponent<PlayerPanic>();
    }

    void Update()
    {
        var v = panic.GetPercent() - 1;
        vig.intensity.value = v;
        ca.intensity.value = v;
    }
}
