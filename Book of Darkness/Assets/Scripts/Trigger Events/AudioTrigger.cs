using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip clip;

    void OnTriggerEnter2D()
    {
        AudioManager.instance.PlaySound(clip, transform.position);
    }
}
