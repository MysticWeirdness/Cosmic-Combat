using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BunkerScript : MonoBehaviour
{
    private int bunkerHealth = 20;
    private SpriteRenderer spriteRenderer;
    private float hitIndicatorDuration = 0.1f;

    [SerializeField] TextMeshProUGUI bunkerHealthText;

    // Bunker Color
    private Vector4 bunkerColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bunkerColor = spriteRenderer.color;
        bunkerHealthText.text = bunkerHealth.ToString();
    }
    private void BunkerHit()
    {
        bunkerHealth--;
        bunkerHealthText.text = bunkerHealth.ToString();
        StartCoroutine("HitIndicator");
        if (bunkerHealth <= 0)
        {
            BunkerDeath();
        }
    }

    private IEnumerator HitIndicator()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hitIndicatorDuration);
        spriteRenderer.color = bunkerColor;
    }
    private void BunkerDeath()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            BunkerHit();
            Destroy(collision.gameObject);
        }
    }
}
