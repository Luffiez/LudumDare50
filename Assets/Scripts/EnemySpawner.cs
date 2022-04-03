using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [Header("Initial Level Spawn Lists")]
    [SerializeField] EnemyWave[] enemyWaves = new EnemyWave[1];


    [Header("After Initial Levels")]
    [SerializeField] EnemyPool enemyPool = new EnemyPool();
    int waveCount = 0;


    private void Start()
    {
        StartCoroutine(SpawnInitialWave());
    }

    IEnumerator SpawnInitialWave()
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            Debug.Log($"Spawn initial wave: {i}");
            EnemyWave wave = enemyWaves[i];
            for (int j = 0; j < enemyWaves[i].enemies.Length; j++)
            {
                SpawnEnemy(wave.enemies[j].prefab);
                yield return new WaitForSeconds(1f / wave.spawnRate);
            }
            yield return new WaitForSeconds(1f / wave.waveCooldown);
        }
        waveCount = enemyWaves.Length;
        StartCoroutine(SpawnAfterWaves());
    }

    IEnumerator SpawnAfterWaves()
    {
        Debug.Log("Start spawning randomly from enemy pool!");

        bool spawning = true;
        while (spawning)
        {
            for (int i = 0; i < enemyPool.waveSize; i++)
            {
                int index = UnityEngine.Random.Range(0, enemyPool.enemies.Length);
                SpawnEnemy(enemyPool.enemies[index].prefab);
                yield return new WaitForSeconds(enemyPool.spawnRate);
            }
            yield return new WaitForSeconds(enemyPool.waveCooldown);
            enemyPool.NewWave();
            waveCount++;
            Debug.Log($"Wave {waveCount} spawned!");
        }
    }


    Vector3 GetRandomSpawnPosition()
    {
        int id = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[id].position;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, GetRandomSpawnPosition(), Quaternion.identity);
    }
}



