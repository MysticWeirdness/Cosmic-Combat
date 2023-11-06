using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject enemyBulletPrefab;

    [Header("Values")]
    private float duration = 1f;

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
        yield return new WaitForSeconds(duration);
        Shoot();
    }
}
