using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;

    private List<SpawnObject> spawnObjects = new List<SpawnObject>();

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        foreach (WaveConfigSO waveConfigSO in waveConfigs)
        {
            WaveConfigToSpawnObjectsConverter(waveConfigSO);
            ShuffleSpawnObjects();
            for (int j = 0; j < spawnObjects.Count; j++)
            {
                Instantiate(spawnObjects[j]);
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void WaveConfigToSpawnObjectsConverter(WaveConfigSO waveConfigSO)
    {
        spawnObjects.Clear();
        foreach (SubWave subWave in waveConfigSO.subWaves)
        {
            for (int i = 0; i < subWave.objectCount; i++)
            {
                spawnObjects.Add(subWave.objectPrefab);
            }
        }
    }

    private void ShuffleSpawnObjects()
    {
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            int randomIndex = Random.Range(0, spawnObjects.Count);
            SpawnObject value = spawnObjects[randomIndex];
            spawnObjects[randomIndex] = spawnObjects[i];
            spawnObjects[i] = value;
        }
    }
}