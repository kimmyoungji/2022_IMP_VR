using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PresistentSingleton<AudioManager>  // �̳г־õ���
{
    [SerializeField] AudioSource sFXPlayer;

    const float MIN_PITCH = 0.75f;  // ��С����(����)
    const float MAX_PITCH = 0.9f;  // �������(����)

    // Used for UI SFX // ����ʵ�� - ʵ����Ч����, �ʺϲ���UI��Ч
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.Volume);
    }

    // Used for repeat-play SFX // �����Ż� - ���������Ч(�ı�����),�ʺϲ��������ظ�����Ч
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }

    // ������չ - ʹ������ʵ���ӵ�����ʱ�������һ����Ч
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlaySFX(audioData[Random.Range(0, audioData.Length)]);
    }
}