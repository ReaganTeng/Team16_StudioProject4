using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    public AudioSource ShootAudioSource;
    public AudioSource ReloadAudioSource;

    public void PlayShootSound()
    {
        ShootAudioSource.Play();
    }
    public void PlayReloadSound()
    {
        ReloadAudioSource.Play();
    }
}
