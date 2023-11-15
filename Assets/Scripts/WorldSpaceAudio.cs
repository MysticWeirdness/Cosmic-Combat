using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("Music Volume");
        audioSource.Play();
    }

    public void StopBGMusic()
    {
        audioSource.Stop();
    }

    public void PlayLoseSFX()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SFX Volume");
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void StartBGMusic()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Music Volume");
        audioSource.Play();
    }
}
