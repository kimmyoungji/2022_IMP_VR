using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : PresistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioClip[] backGroundMusics;

    const float MIN_PITCH = 0.75f;
    const float MAX_PITCH = 0.9f;

    // Play BGM
    private void Start()
    {
        musicPlayer.loop = true;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicPlayer.clip = backGroundMusics[0];
            print("Play BGM 1");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            musicPlayer.clip = backGroundMusics[1];
            print("Play BGM 2");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            musicPlayer.clip = backGroundMusics[2];
            print("Play BGM 3");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            musicPlayer.clip = backGroundMusics[3];
            print("Play BGM 4");
        }
    }

    // Used for UI SFX
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.Volume);
    }

    // Used for repeat-play SFX
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }

    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlaySFX(audioData[Random.Range(0, audioData.Length)]);
    }
}