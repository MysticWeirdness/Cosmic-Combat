using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidBody;

    [Header("Values")]
    private float speed = 5f;
    private float bulletBounds;
    private bool isPlayerBullet;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if(gameObject.tag == "PlayerBullet")
        {
            isPlayerBullet = true;
        }
        else if (gameObject.tag == "EnemyBullet")
        {
            isPlayerBullet = false;
        }
        if (isPlayerBullet)
        {
            rigidBody.velocity = Vector3.up * speed;
            bulletBounds = 6f;
        }
        else if(!isPlayerBullet)
        {
            rigidBody.velocity = Vector3.down * speed;
            bulletBounds = -6f;
        }
    }

    private void Update()
    {
        if (isPlayerBullet)
        {
            if(transform.position.y >= bulletBounds)
            {
                Destroy(gameObject);
            }
        }
        else if (!isPlayerBullet)
        {
            if(transform.position.y <= bulletBounds)
            {
                Destroy(gameObject);
            }
        }
    }
}
