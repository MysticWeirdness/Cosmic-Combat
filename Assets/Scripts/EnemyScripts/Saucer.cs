using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    [SerializeField] private GameObject thousandEffect; 
    private Rigidbody2D rigidBody;
    private float speed = 5f;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(Instantiate(thousandEffect, transform.position, Quaternion.identity), 0.5f);
        }
    }
}
