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
    private int shop = 6;
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(mainGame);
        Time.timeScale = 1.0f;
    }

    public void SettingsButton()
    {
        SceneManager.LoadSceneAsync(settings);
        Time.timeScale = 1.0f;
    }

    public void CreditsButton()
    {
        SceneManager.LoadSceneAsync(credits);
        Time.timeScale = 1.0f;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(menu);
        Time.timeScale = 1.0f;
    }

    public void ShopButton()
    {
        SceneManager.LoadSceneAsync(shop);
        Time.timeScale = 1.0f;
    }
}
