using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;
    private Transform muzzle;
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private GameController gameController;
    [SerializeField] private List<GameObject> livesUI = new List<GameObject>();

    [Header("Values")]
    private int totalLives = 3;
    private int currentLives;
    private int powerLevel;
    private float leftLimit = -8.5f;
    private float rightLimit = 8.5f;
    private float moveSpeed = 5f;
    private float xInput;
    [SerializeField] private bool STDMovement;

    [Header("Prefabs")]
    [SerializeField] private GameObject explosionPrefab;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        muzzle = GetComponent<Transform>().GetChild(2);
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        int x = PlayerPrefs.GetInt("Standard Movement");
        if(x == 1)
        {
            STDMovement = true;
        }
        else if(x == 0)
        {
            STDMovement = false;
        }
        currentLives = totalLives;
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");

        if (STDMovement)
        {
            if(transform.position.x >= rightLimit)
            {
                rigidBody.velocity = Vector3.zero;
                if(xInput < 0)
                {
                    rigidBody.velocity = new Vector2(xInput, rigidBody.velocity.y) * moveSpeed;
                }
            }
            else if(transform.position.x <= leftLimit)
            {
                rigidBody.velocity = Vector3.zero;
                if (xInput > 0)
                {
                    rigidBody.velocity = new Vector2(xInput, rigidBody.velocity.y) * moveSpeed;
                }
            }
            else
            {
                rigidBody.velocity = new Vector2(xInput, rigidBody.velocity.y) * moveSpeed;
            }
        }
        else if(!STDMovement)
        {
            if (transform.position.x >= rightLimit)
            {
                if (xInput < 0)
                {
                    rigidBody.AddForce(Vector2.right * xInput * moveSpeed, ForceMode2D.Force);
                }
            }
            else if (transform.position.x <= leftLimit)
            {
                if (xInput > 1)
                {
                    rigidBody.AddForce(Vector2.right * xInput * moveSpeed, ForceMode2D.Force);
                }
            }
            else
            {
                rigidBody.AddForce(Vector2.right * xInput * moveSpeed, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            PlayerHit();
        }
    }

    private void PlayerHit()
    {
        currentLives--;
        audioSource.PlayOneShot(audioClips[0]);
        if(currentLives >= 0)
        {
            Destroy(livesUI[currentLives]);
        }
        else if (currentLives < 0)
        {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 0.3f);
            gameController.StartCoroutine("PlayerDeathOffsetTimer");
        }
    }
}
