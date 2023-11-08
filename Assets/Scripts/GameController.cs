using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject EnemyGroup;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private List<EnemyScript> enemyScripts = new List<EnemyScript>();
    private List<Transform> enemiesInGroup = new List<Transform>();
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private GameObject player;

    [Header("Values")]
    private float cooldownDuration;
    private bool paused = false;

    [Header("EnemyMovement")]
    private float speed = 1f;
    private int moveDirection = 1;
    private float leftLimit = -8.5f;
    private float rightLimit = 8.5f;
    private float farthestLeftEnemy = 0f;
    private float farthestRightEnemy = 0f;

    private void Start()
    {
        for(int j = 0; j < 7; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(enemies[j], new Vector3(i - 4f, j - 2f, 0f), Quaternion.identity, EnemyGroup.transform);
            }
        }
        enemyScripts = EnemyGroup.transform.GetComponentsInChildren<EnemyScript>().ToList();
        enemiesInGroup = EnemyGroup.transform.GetComponentsInChildren<Transform>().ToList();
        RandomizeEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else if (paused)
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
    }
    public void KillPlayer()
    {
        Destroy(player);
        DeathUI.SetActive(true);
    }

    private void RandomizeEnemy()
    {
        enemyScripts.RemoveAll(s => s == null);
        int listLength = enemyScripts.Count;
        int randomEnemy = UnityEngine.Random.Range(0, listLength);
        enemyScripts[randomEnemy].Shoot();
        StartCoroutine("Cooldown");
    }
    private IEnumerator Cooldown()
    {
        cooldownDuration = UnityEngine.Random.value;
        yield return new WaitForSeconds(cooldownDuration);
        RandomizeEnemy();
    }

    private void MoveEnemyGroup()
    {
        
    }
}
