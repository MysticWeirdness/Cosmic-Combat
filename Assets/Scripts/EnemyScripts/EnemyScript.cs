using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject explosion3;
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    [Header("Values")]
    [SerializeField] private int health;
    private float hitIndicatorDuration = 0.1f;
    [SerializeField] private int value;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Shoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    }

    private void Hit()
    {
        health--;
        audioSource.volume = PlayerPrefs.GetFloat("SFX Volume");
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine("HitIndicator");
        if(health == 0)
        {
            Death();
        }
    }

    private IEnumerator HitIndicator()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hitIndicatorDuration);
        spriteRenderer.color = Color.white;
    }
    private void Death()
    {
        Destroy(Instantiate(explosion3, transform.position, Quaternion.identity), 0.3f);
        gameController.AddScore(value);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Hit();
        }
    }
}
