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

    public static int EnemiesOnScreen = 0;

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

    void WinGame()
    {
        Debug.Log("you won!");
        GameManager.GameEnded = true;
    }

    void UpdateWave()
    {
        if (OnFinalWave) return;

        currentWave = waveList[waveIndex];
        countdown = currentWave.waveDelay;
    }

    void Update()
    {
        if (EnemiesOnScreen > 0 || GameManager.GameEnded) return;

        if (OnFinalWave && !GameManager.GameEnded)
        {
            WinGame();
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
        EnemiesOnScreen++;
    }

    IEnumerator SpawnWave()
    {
        Player.RoundsSurvived++;

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
