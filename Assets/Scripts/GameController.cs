using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject Scythe;

    private void Start()
    {
        Instantiate(Scythe, new Vector3(0f, 4f, 0f), Quaternion.identity);
    }
}
