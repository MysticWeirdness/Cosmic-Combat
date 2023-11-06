using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableBackground : MonoBehaviour
{
    private GameObject background1;
    private GameObject background2;
    private float scrollingBackgroundSpeed = 50f;

    private void Start()
    {
        background1 = GameObject.FindWithTag("Background1");
        background2 = GameObject.FindWithTag("Background2");
    }

    private void Update()
    {
        if(background1.transform.position.x <= -1920f/2)
        {
            background1.transform.position = background2.transform.position + new Vector3(1920f, 0f, 0f);
        }
        else if (background2.transform.position.x <= -1920f/2)
        {
            background2.transform.position = background1.transform.position + new Vector3(1920f, 0f, 0f);
        }
        background1.transform.position += Vector3.left * scrollingBackgroundSpeed * Time.deltaTime;
        background2.transform.position += Vector3.left * scrollingBackgroundSpeed * Time.deltaTime;
    }
}
