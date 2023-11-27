using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialsScript : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> initials = new List<TextMeshProUGUI>();
    [SerializeField] private List<GameObject> cursors = new List<GameObject>();
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject newHighscore;
    [SerializeField] private HighScoreHandler highScoreHandler;

    private string fullInitials;
    private int currentInitial = 0;
    private bool enteringInitials = true;

    private void Start()
    {
        SetCursor(currentInitial);
    }
    private void Update()
    {
        if (enteringInitials)
        {
            ListenForInitials();
        }
    }
    void ListenForInitials()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    if (Input.GetKey(KeyCode.Backspace))
                    {
                        if(currentInitial == 0)
                        {
                            break;
                        }
                        currentInitial--;
                        SetCursor(currentInitial);
                        return;
                    }
                    else if(kcode.ToString().Length > 1)
                    {
                        return;
                    }
                    if(currentInitial > initials.Count - 1)
                    {
                        return;
                    }
                    initials[currentInitial].text = kcode.ToString();
                    currentInitial++;
                    if (currentInitial > initials.Count - 1)
                    {
                        return;
                    }
                    SetCursor(currentInitial);
                    return;
                }
            }
        }
    }

    private void SetCursor(int initial)
    {
        ClearCursors();
        cursors[initial].SetActive(true);
    }

    private void ClearCursors()
    {
        for (int i = 0; i < 3; i++)
        {
            cursors[i].SetActive(false);
        }
    }
    public void ConfirmInitials()
    {
        fullInitials = initials[0].text + initials[1].text + initials[2].text;
        //       highScoreHandler.onClick(fullInitials, PlayerPrefs.GetInt("Highscore"));
        PlayerPrefs.SetString("HighscoreInitials", fullInitials);
        newHighscore.SetActive(false);
        shop.SetActive(true);
        settings.SetActive(true);
        menu.SetActive(true);
        restart.SetActive(true);
        gameObject.SetActive(false);
    }
}
