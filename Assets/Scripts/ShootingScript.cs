using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting")]
    private bool canShoot = true;
    private float duration = 0.25f;
    private float damage;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private AudioSource shootSoundSource;

    private void Start()
    {
        shootSoundSource.volume = PlayerPrefs.GetFloat("SFX Volume");
    }
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
        Destroy(Instantiate(muzzleFlashPrefab, transform.position, Quaternion.identity), 0.1f);
        shootSoundSource.PlayOneShot(shootSoundSource.clip);
        canShoot = false;
        StartCoroutine("Cooldown");
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(duration);
        canShoot = true;
    }
}
