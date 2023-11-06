using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject Scythe;
    [SerializeField] private GameObject BugEye;
    [SerializeField] private GameObject Crescent;
    [SerializeField] private GameObject Fighter;
    [SerializeField] private GameObject Saucer;
    [SerializeField] private GameObject Slicer;
    [SerializeField] private GameObject Spikey;
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private GameObject player;

    private void Start()
    {
        Instantiate(Scythe, new Vector3(0f, 4f, 0f), Quaternion.identity);
        Instantiate(BugEye, new Vector3(-1f, 4f, 0f), Quaternion.identity);
        Instantiate(Crescent, new Vector3(-2f, 4f, 0f), Quaternion.identity);
        Instantiate(Fighter, new Vector3(-3f, 4f, 0f), Quaternion.identity);
        Instantiate(Saucer, new Vector3(1f, 4f, 0f), Quaternion.identity);
        Instantiate(Slicer, new Vector3(2f, 4f, 0f), Quaternion.identity);
        Instantiate(Spikey, new Vector3(3f, 4f, 0f), Quaternion.identity);
    }
    public void KillPlayer()
    {
        Destroy(player);
        DeathUI.SetActive(true);
    }
}
