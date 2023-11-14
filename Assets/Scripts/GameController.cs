using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject EnemyGroup;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private List<EnemyScript> enemyScripts = new List<EnemyScript>();
    private List<Transform> enemiesInGroup = new List<Transform>();
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private WorldSpaceAudio worldSpaceAudio;

    [Header("Values")]
    private float cooldownDuration;
    private bool paused = false;
    private bool lost = false;
    private int wave = 1;
    private int score = 0;
    private float enemyCooldownMultiplier = 2f;

    [Header("EnemyMovement")]
    private float yDistance = -0.5f;
    private float speed = 1f;
    private int moveDirection = 1;
    private float leftLimit = -8.5f;
    private float rightLimit = 8.5f;
    private float farthestLeftEnemy = 0f;
    private float farthestRightEnemy = 0f;
    private float lowestEnemy = 0f;
    private bool right = true;

    [Header("Vectors")]
    private Vector3 yDistanceVector;

    private void Start()
    {
        worldSpaceAudio = GameObject.FindWithTag("WorldSpaceAudio").GetComponent<WorldSpaceAudio>();
        yDistanceVector = new Vector3(0f, yDistance, 0f);
        GenerateEnemies();
        RandomizeEnemy();
    }

    // This function will generate a new enemy group
    private void GenerateEnemies()
    {
        EnemyGroup.transform.position = Vector3.zero;
        for (int j = 0; j < enemies.Count; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(enemies[j], new Vector3(i - 4f, j, 0f), Quaternion.identity, EnemyGroup.transform);
            }
        }
        enemyScripts = EnemyGroup.transform.GetComponentsInChildren<EnemyScript>().ToList();
        enemiesInGroup = EnemyGroup.transform.GetComponentsInChildren<Transform>().ToList();
    }
    private void Update()
    {
        if(enemiesInGroup.Count == 1)
        {
            wave++;
            GenerateEnemies();
        }
        MoveEnemyGroup();
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
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
    }
    private void UpdateUI()
    {
        waveText.text = "Wave: " + wave.ToString();
        scoreText.text = score.ToString("00000000");
    }

    // Called when the game is paused
    private void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    // Called when the game is unpaused
    public void Unpause()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    // Called when the player loses
    private void Lose()
    {
        worldSpaceAudio.StopBGMusic();
        worldSpaceAudio.PlayLoseSFX();
        Time.timeScale = 0;
        DeathUI.SetActive(true);
    }

    // Starts the coroutine when the player dies
    public IEnumerator PlayerDeathOffsetTimer()
    {
        lost = true;
        Destroy(player);
        yield return new WaitForSeconds(0.5f);
        Lose();
    }

    // This function will choose a random enemy out of all the enemies to fire a bullet
    private void RandomizeEnemy()
    {
        enemyScripts.RemoveAll(s => s == null);
        int listLength = enemyScripts.Count;
        int randomEnemy = UnityEngine.Random.Range(0, listLength);
        enemyScripts[randomEnemy].Shoot();
        StartCoroutine("Cooldown");
    }

    // This is used for the enemy's cooldown inbetween each shot
    private IEnumerator Cooldown()
    {
        cooldownDuration = UnityEngine.Random.value * enemyCooldownMultiplier;
        yield return new WaitForSeconds(cooldownDuration);
        RandomizeEnemy();
    }

    // This function will move the entire group of enemies left, right, and down
    private void MoveEnemyGroup()
    {
        enemiesInGroup.RemoveAll(s => s == null);
        foreach (Transform E in enemiesInGroup)
        {
            if(E.position.x >= farthestRightEnemy)
            {
                farthestRightEnemy = E.position.x;
            }
        }

        foreach(Transform E in enemiesInGroup)
        {
            if (E.position.x <= farthestLeftEnemy)
            {
                farthestLeftEnemy = E.position.x;
            }
        }

        foreach (Transform E in enemiesInGroup)
        {
            if (E.position.y <= lowestEnemy)
            {
                lowestEnemy = E.position.y;
            }
        }
        if (!lost)
        {
            if (lowestEnemy <= player.transform.position.y - 1f)
            {
                Lose();
            }
        }
        
        if (farthestRightEnemy >= rightLimit && right == true)
        {
            EnemyGroup.transform.position += yDistanceVector;
            moveDirection = -1;
            right = false;
        }
        if(farthestLeftEnemy <= leftLimit && right == false)
        {
            EnemyGroup.transform.position += yDistanceVector;
            moveDirection = 1;
            right = true;
        }
        EnemyGroup.transform.position += new Vector3(Time.deltaTime * moveDirection * speed, 0f, 0f);
        farthestRightEnemy = -100f;
        farthestLeftEnemy = 100f;
        lowestEnemy = 0;
    }
}
