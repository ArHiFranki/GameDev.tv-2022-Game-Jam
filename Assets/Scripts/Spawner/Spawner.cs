using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<WaveConfigSO> waveConfigs;

    private List<SpawnObject> spawnObjects = new List<SpawnObject>();
    private int currentLevel;
    private float currentSpawnRate;
    private int[] previousPoints = new int[4];

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
                int spawnPointNumber = GenerateSpawnPointNumber();
                Instantiate(spawnObjects[i], spawnPoints[spawnPointNumber].position, Quaternion.identity, transform);
                spawnObjects[i].GetComponent<ObjectMover>().InitSpeedController(speedController);
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

    private int GenerateSpawnPointNumber()
    {
        bool isAllPointsEqual = true;
        int amountOfPoints = spawnPoints.Count;
        int spawnPointNumber = Random.Range(0, amountOfPoints);

        for (int i = amountOfPoints - 1; i > 0; i--)
        {
            previousPoints[i] = previousPoints[i - 1];
        }

        previousPoints[0] = spawnPointNumber;

        for (int i = 0; i < amountOfPoints; i++)
        {
            if (previousPoints[i] != previousPoints[0])
            {
                isAllPointsEqual = false;
            }
        }

        if (isAllPointsEqual)
        {
            while (spawnPointNumber == previousPoints[0])
            {
                spawnPointNumber = Random.Range(0, amountOfPoints);
            }
            previousPoints[0] = spawnPointNumber;
        }

        return spawnPointNumber;
    }

    private void OnSpeedChange()
    {
        currentSpawnRate = distanceBetweenObjects / speedController.CurrentSpeed;

        Debug.Log("Level: " + currentLevel +
                  "   Speed: " + speedController.CurrentSpeed +
                  "   SpawnRate: " + currentSpawnRate);
    }
}