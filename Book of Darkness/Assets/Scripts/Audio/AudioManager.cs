using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel { Master, Sfx, Bgm };

    float masterPercent = 1;
    float sfxPercent = 2;
    float bgmPercent = .3f;

    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    public static AudioManager instance;

    Transform audioListener;
    Transform player;

    SoundLibrary library;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        audioListener = FindObjectOfType<AudioListener>().transform;
        player = FindObjectOfType<Player>().transform;
        library = GetComponent<SoundLibrary>();

        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            musicSources[i].loop = true;

            newMusicSource.transform.parent = transform;
        }

        masterPercent = PlayerPrefs.GetFloat("master vol", masterPercent);
        sfxPercent = PlayerPrefs.GetFloat("sfx vol", sfxPercent);
        bgmPercent = PlayerPrefs.GetFloat("bgm vol", bgmPercent);
    }

    void Update()
    {
        if (player != null)
            audioListener.position = player.position;
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterPercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxPercent = volumePercent;
                break;
            case AudioChannel.Bgm:
                bgmPercent = volumePercent;
                break;
        }

        musicSources[0].volume = bgmPercent * masterPercent;
        musicSources[1].volume = bgmPercent * masterPercent;

        PlayerPrefs.SetFloat("master vol", masterPercent);
        PlayerPrefs.SetFloat("sfx vol", sfxPercent);
        PlayerPrefs.SetFloat("bgm vol", bgmPercent);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
            PlayClipAtPoint(clip, pos, sfxPercent * masterPercent, Random.Range(0.8f, 1.2f));
    }

    public void PlaySound(string name, Vector3 pos)
    {
        PlaySound(library.GetClip(name), pos);
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, bgmPercent * masterPercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(bgmPercent * masterPercent, 0, percent);
            yield return null;
        }
    }

    // Override method for pitch control
    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitch, bool loop = false)
    {
        GameObject obj = new GameObject();
        obj.transform.position = position;
        var audioSrc = obj.AddComponent<AudioSource>();
        audioSrc.pitch = pitch;
        audioSrc.PlayOneShot(clip, volume);
        if (loop)
        {
            audioSrc.loop = true;
        }
        else
        {
            Destroy(obj, clip.length / pitch);
        }
        return audioSrc;
    }

    public AudioSource PlayClipAtPoint(string name, Vector3 position, float volume, float pitch, bool loop = false)
    {
        var clip = library.GetClip(name);

        GameObject obj = new GameObject();
        obj.transform.position = position;
        var audioSrc = obj.AddComponent<AudioSource>();
        audioSrc.pitch = pitch;
        audioSrc.PlayOneShot(clip, volume);
        if (loop)
        {
            audioSrc.loop = true;
        }
        else
        {
            Destroy(obj, clip.length / pitch);
        }
        return audioSrc;
    }
}
