using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public float waveDelay = 3f;
    public float enemyDelay = 0.5f;
    public int enemyCount = 1;
    public GameObject enemyObj;
}

public class WaveManager : MonoBehaviour
{
    public Transform enemyPrefab;
    private Transform spawnLocation;
    public List<Wave> waveList;
    public TMP_Text waveTimer;
    public GameManager game;

    public static int EnemiesToKill = 0;

    private float countdown;
    private bool spawning = false;
    private int waveIndex;
    private Wave currentWave;

    public bool OnFinalWave { get { return waveIndex >= waveList.Count; } }

    void Awake()
    {
        spawnLocation = GameObject.FindGameObjectWithTag("Spawn").transform;
    }

    void Start()
    {
        waveIndex = 0;
        UpdateWave();
    }


    void UpdateWave()
    {
        if (OnFinalWave) return;

        currentWave = waveList[waveIndex];
        countdown = currentWave.waveDelay;
    }

    void Update()
    {
        if (EnemiesToKill > 0) return;

        if (OnFinalWave)
        {
            game.Win();
            this.enabled = false;
            return;
        }

        if (countdown <= 0f) StartCoroutine(SpawnWave());

        if (!spawning)
        {
            waveTimer.text = string.Format("{0:00.00}", countdown);
            countdown -= Time.deltaTime;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnLocation.position, spawnLocation.rotation);
    }

    IEnumerator SpawnWave()
    {
        Player.RoundsSurvived++;
        EnemiesToKill = currentWave.enemyCount;

        waveTimer.text = "";
        spawning = true;
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyObj);
            yield return new WaitForSeconds(currentWave.enemyDelay);
        }
        spawning = false;

        waveIndex++;
        UpdateWave();
    }

    public void OnValidate()
    {
        // TODO: Validate 
    }
}
