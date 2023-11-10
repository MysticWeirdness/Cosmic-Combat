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
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject player;
    private WorldSpaceAudio worldSpaceAudio;

    [Header("Values")]
    private float cooldownDuration;
    private bool paused = false;

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

    private void GenerateEnemies()
    {
        for (int j = 0; j < 7; j++)
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
    }

    private void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Unpause()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    private void Lose()
    {
        worldSpaceAudio.StopBGMusic();
        worldSpaceAudio.PlayLoseSFX();
        Time.timeScale = 0;
        DeathUI.SetActive(true);
    }


    public IEnumerator PlayerDeathOffsetTimer()
    {
        Destroy(player);
        yield return new WaitForSeconds(0.5f);
        Lose();
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

/*        if(lowestEnemy <= -5f)
        {
            Lose();
        }*/
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
