using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private TMP_Dropdown difficulty;
    [SerializeField] private Toggle standardMovement;
    [SerializeField] private Toggle oldEnemyMovement; 

    private void Start()
    {
        int x = PlayerPrefs.GetInt("Standard Movement");
        int y = PlayerPrefs.GetInt("OldEnemyMovement");
        if (x == 1)
        {
            standardMovement.isOn = true;
        }
        else if(x == 0)
        {
            standardMovement.isOn = false;
        }
        if(y == 1)
        {
            oldEnemyMovement.isOn = true;
        }
        if (y == 0)
        {
            oldEnemyMovement.isOn = false;
        }
        sfxVolume.value = PlayerPrefs.GetFloat("SFX Volume");
        musicVolume.value = PlayerPrefs.GetFloat("Music Volume");
    }
    public void StandardMovement()
    {
        int i = PlayerPrefs.GetInt("Standard Movement");
        if (standardMovement.isOn)
        {
            i = 1;
        }
        else if (!standardMovement.isOn)
        {
            i = 0;
        }
        PlayerPrefs.SetInt("Standard Movement", i);
    }

    public void OldEnemyMovement()
    {
        int i = PlayerPrefs.GetInt("OldEnemyMovement");
        if (oldEnemyMovement.isOn)
        {
            i = 1;
        }
        else if (!oldEnemyMovement.isOn)
        {
            i = 0;
        }
        PlayerPrefs.SetInt("OldEnemyMovement", i);
    }
    public void Difficulty()
    {
        PlayerPrefs.SetString("Difficulty", difficulty.value.ToString());
    }

    public void SFXVolume()
    {
        PlayerPrefs.SetFloat("SFX Volume", sfxVolume.value);
    }
    public void MusicVolume()
    {
        PlayerPrefs.SetFloat("Music Volume", musicVolume.value);
    }
    public void Save()
    {
        PlayerPrefs.Save();
    }
}
