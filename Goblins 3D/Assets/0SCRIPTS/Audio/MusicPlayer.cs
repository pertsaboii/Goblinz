using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    /*
    OHJE:
    - tämä script toistaa taustamusiikkia/ambienssia
    - toimii myös pelkällä looppaavalla klipillä, jos on intro erikseen niin toistaa ensin intron kerran ja sen jälkeen alkaa looppaamaan looppia
    */

    [SerializeField] private AudioClip musicIntroClip;
    [SerializeField] private AudioClip musicLoopClip;

    private void Awake()
    {
        SoundManager.Instance.musicSounds.clip = musicLoopClip;
        PlayMusic();
    }

    // sceneen kuuluvan musiikin toisto
    private void PlayMusic()
    {
        if (musicIntroClip != null)
        {
            SoundManager.Instance.musicSounds.PlayOneShot(musicIntroClip);
            SoundManager.Instance.musicSounds.PlayScheduled(AudioSettings.dspTime + musicIntroClip.length);
        }
        else if (musicLoopClip != null) SoundManager.Instance.musicSounds.Play();
    }
}
