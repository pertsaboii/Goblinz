using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixer masterMixer;
    [HideInInspector] public float SFXVol, UIVol, MasterVol, MusicVol;

    [SerializeField] private AudioSource UISounds, SFXSounds;
    public AudioSource musicSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        UISounds.ignoreListenerPause = true;
    }

    // ��nien toisto
    public void PlaySFXSound(AudioClip clip)
    {
        SFXSounds.PlayOneShot(clip);
    }
    public void PlayUISound(AudioClip clip)
    {
        UISounds.PlayOneShot(clip);
    }
    public void PlayMusicSound(AudioClip clip)
    {
        musicSounds.PlayOneShot(clip);
    }

    // voluumien s��t�
    public void ChangeSFXVolume(float value)
    {
        masterMixer.SetFloat("SFXVol", value);
        SFXVol = value;
    }
    public void ChangeMasterVolume(float value)
    {
        masterMixer.SetFloat("MasterVol", value);
        MasterVol = value;
    }
    public void ChangeUIVolume(float value)
    {
        masterMixer.SetFloat("UIVol", value);
        UIVol = value;
    }
    public void ChangeMusicVolume(float value)
    {
        masterMixer.SetFloat("MusicVol", value);
        MusicVol = value;
    }
}
