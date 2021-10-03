using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip bgm;

    void Start()
    {
        AudioManager.instance.PlayMusic(bgm, 2);
    }
}
