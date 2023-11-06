using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [Header("Scenes")]
    private int menu = 2;
    private int mainGame = 3;
    private int settings = 4;
    private int credits = 5;
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(mainGame);
    }

    public void SettingsButton()
    {
        SceneManager.LoadSceneAsync(settings);
    }

    public void CreditsButton()
    {
        SceneManager.LoadSceneAsync(credits);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(menu);
    }
}
