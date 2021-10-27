using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip clip;
    public float vol = 1;
    public bool loop = false;

    void OnTriggerEnter2D()
    {
        AudioManager.instance.PlayClipAtPoint(clip, transform.position, vol, 1, loop);
        Destroy(gameObject);
    }
}
