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
    }

    public void StopBGMusic()
    {
        audioSource.Stop();
    }

    public void PlayLoseSFX()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void StartBGMusic()
    {
        audioSource.Play();
    }
}
