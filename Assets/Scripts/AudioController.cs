using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip intenseBossMusic;
    public AudioClip sadMusic;
    public AudioClip homepageMusic;
    public AudioClip twinBossMusic;
    public AudioClip happyMusic;
    public AudioClip normalMusic;
    public AudioClip normalMusicHighTempo;

    public void PlayIntenseBoss()
    {
        audioSource.Pause();
        audioSource.clip = intenseBossMusic; 
        audioSource.Play();
    }

    public void PlaySadMusic()
    {
        audioSource.Pause();
        audioSource.clip = sadMusic;
        audioSource.Play();
    }

    public void PlayHappyMusic()
    {
        audioSource.Pause();
        audioSource.clip = happyMusic; 
        audioSource.Play();
    }

    public void PlayTwinBossMusic()
    {
        audioSource.Pause();
        audioSource.clip = twinBossMusic;   
        audioSource.Play();
    }

    public void PlayHomepageMusic()
    {
        audioSource.Pause();
        audioSource.clip = homepageMusic;
        audioSource.Play();
    }

    public void PlayNormalMusic()
    {
        audioSource.Pause();
        audioSource.clip = normalMusic;
        audioSource.Play();
    }
    
    public void PlayNormalMusicHighTempo()
    {
        audioSource.Pause();
        audioSource.clip = normalMusicHighTempo;
        audioSource.Play();
    }
}
