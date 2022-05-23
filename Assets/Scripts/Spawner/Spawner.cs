using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private List<WaveConfigSO> waveConfigs;

    private List<SpawnObject> spawnObjects = new List<SpawnObject>();
    private int currentLevel;
    private float currentSpawnRate;

    public int CurrentLevel => currentLevel;

    public event UnityAction LevelChange;

    private void OnEnable()
    {
        speedController.SpeedChange += OnSpeedChange;
    }

    private void OnDisable()
    {
        speedController.SpeedChange -= OnSpeedChange;
    }

    private void Start()
    {
        currentLevel = 0;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        foreach (WaveConfigSO waveConfigSO in waveConfigs)
        {
            currentLevel++;
            LevelChange?.Invoke();

            WaveConfigToSpawnObjectsConverter(waveConfigSO);
            ShuffleSpawnObjects();
            for (int i = 0; i < spawnObjects.Count; i++)
            {
                Instantiate(spawnObjects[i]);
                yield return new WaitForSeconds(currentSpawnRate);
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

    private void OnSpeedChange()
    {
        currentSpawnRate = distanceBetweenObjects / speedController.CurrentSpeed;

        Debug.Log("Level: " + currentLevel +
                  "   Speed: " + speedController.CurrentSpeed +
                  "   SpawnRate: " + currentSpawnRate);
    }
}