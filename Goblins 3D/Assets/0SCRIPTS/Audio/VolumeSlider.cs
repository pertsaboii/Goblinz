using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private bool UIVolumeSlider, masterVolumeSlider, SFXVolumeSlider, musicVolumeSlider;
    void Start()
    {
        slider = GetComponent<Slider>();

        if (masterVolumeSlider)
        {
            slider.value = SoundManager.Instance.MasterVol;
            slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMasterVolume(value));
        }
        else if (UIVolumeSlider)
        {
            slider.value = SoundManager.Instance.UIVol;
            slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeUIVolume(value));
        }
        else if (SFXVolumeSlider)
        {
            slider.value = SoundManager.Instance.SFXVol;
            slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeSFXVolume(value));
        }
        else if (musicVolumeSlider)
        {
            slider.value = SoundManager.Instance.MusicVol;
            slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMusicVolume(value));
        }
    }
}
