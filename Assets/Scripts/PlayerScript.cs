using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnimator;
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnimator.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnimator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnimator.SetBool("Right", false);
        }
    }
}
