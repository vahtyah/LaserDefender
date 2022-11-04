using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            for (int i = 0; i < waveConfigs.Count; i++)
            {
                currentWave = waveConfigs[i];
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(0), currentWave.GetStartingWaypoint().position, Quaternion.identity, transform);
                    yield return new WaitForSeconds(currentWave.getRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        } while (isLooping);
    }
}
