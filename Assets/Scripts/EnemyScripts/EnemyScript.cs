using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject enemyBulletPrefab;

    [Header("Values")]
    private float cooldownDuration = 1f;
    private int health = 1;
    private void Start()
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        StartCoroutine("Cooldown");
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        Shoot();
    }

    private void Hit()
    {
        health--;
        if(health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
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
