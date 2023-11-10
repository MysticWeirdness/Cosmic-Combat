using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrollingScript : MonoBehaviour
{
    [Header("GameObjects")]

    [SerializeField] private List<GameObject> backgrounds = new List<GameObject>();

    [Header("Values")]
    private float speed = 0.5f;

    private void Start()
    {
        for(int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].transform.position = Vector3.up * (i - 1) * 10f;
        }
    }
    private void Update()
    {
        CheckBackgrounds();
        for(int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void CheckBackgrounds()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            if (backgrounds[i].transform.position.y <= -10f)
            {
                backgrounds[i].transform.position = Vector3.up * (backgrounds.Count - 1) * 10f;
            }
        }
    }
}
