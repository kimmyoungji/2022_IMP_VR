using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsAudioPlayer : MonoBehaviour
{
    public AudioClip brokenFireClip;
    private AudioSource brokenSource;

    const float MIN_PITCH = 0.85f;
    const float MAX_PITCH = 0.98f;

    void Awake()
    {
        brokenSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        brokenSource.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        brokenSource.PlayOneShot(brokenFireClip);
    }
}
