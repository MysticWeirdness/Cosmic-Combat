using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoVideoHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private int nextScene;
    private void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
        StartCoroutine("LoadSceneOnVideoEnd");
    }

    private IEnumerator LoadSceneOnVideoEnd()
    {
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
        SceneManager.LoadSceneAsync(nextScene);
    }
}
