using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public Transform enemyPrefab;
    private Transform spawnLocation;
    public float timeBetweenWaves = 5f;
    public float timeBetweenEnemies = 0.5f;

    public TMP_Text waveTimer;

    private float countdown = 2f;
    private bool spawning = false;

    private int prevEnemyCount = 0;
    private int enemyCount = 1;

    void Awake()
    {
        spawnLocation = GameObject.FindGameObjectWithTag("Spawn").transform;
    }

    void IncreaseEnemies()
    {
        int temp = enemyCount;
        enemyCount += prevEnemyCount;
        prevEnemyCount = temp;

        timeBetweenEnemies = 1f - Mathf.Log(Player.RoundsSurvived + 0.01f) / 3;
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        if (!spawning)
        {
            waveTimer.text = string.Format("{0:00.00}", countdown);
            countdown -= Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation);
    }

    IEnumerator SpawnWave()
    {
        Player.RoundsSurvived++;

        waveTimer.text = "";
        spawning = true;
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        spawning = false;

        IncreaseEnemies();
    }

    public void OnValidate()
    {
        timeBetweenWaves = Mathf.Max(1f, timeBetweenWaves);
        timeBetweenEnemies = Mathf.Max(0.1f, timeBetweenEnemies);
    }
}
