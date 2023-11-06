using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting")]
    private bool canShoot = true;
    private float duration = 0.5f;
    private float damage;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Instantiate(muzzleFlashPrefab, transform.position, Quaternion.identity);
        canShoot = false;
        StartCoroutine("Cooldown");
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(duration);
        canShoot = true;
    }
}
