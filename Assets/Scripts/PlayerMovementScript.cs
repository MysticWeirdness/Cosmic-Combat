using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private Transform muzzle;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private GameController gameController;

    [Header("Values")]
    private int totalLives = 3;
    private int currentLives;
    private int powerLevel;
    private float leftLimit;
    private float rightLimit;
    private float moveSpeed = 5f;
    private float xInput;
    public bool STDMovement = false;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        muzzle = GetComponent<Transform>().GetChild(2);
        audioSource = GetComponent<AudioSource>();
        audioClip = GetComponent<AudioClip>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currentLives = totalLives;
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        if (STDMovement)
        {
            rigidBody.velocity = new Vector2 (xInput, rigidBody.velocity.y) * moveSpeed;
        }
        else if(!STDMovement)
        {
            rigidBody.AddForce(Vector2.right * xInput * moveSpeed, ForceMode2D.Force);
        }
    }
}
