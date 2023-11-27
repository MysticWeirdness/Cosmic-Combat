using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();
    [SerializeField] string filename;

    private void Start()
    {
        entries = File_Handler.readListFromJson<InputEntry>(filename);
    }

    public void addNameToList(string initials)
    {
        entries.Add(new InputEntry(initials, Random.Range(0,100)));
        initials = string.Empty;

        File_Handler.saveListToJson<InputEntry>(entries,filename);
    }
}
