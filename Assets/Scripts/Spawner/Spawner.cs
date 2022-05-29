using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private ScoreKeeper scoreKeeper;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<WaveConfigSO> waveConfigs;

    private List<SpawnObject> spawnObjects = new List<SpawnObject>();
    private int currentLevel;
    private int spawnPointNumber;
    private float currentSpawnRate;
    private int[] previousPoints = new int[4];
    private Vector3 spawnPointPosition;
    private bool isSpawnEnable;

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
        isSpawnEnable = true;
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
                if (!isSpawnEnable)
                {
                    break;
                }

                spawnPointNumber = GenerateSpawnPointNumber();
                spawnPointPosition = spawnPoints[spawnPointNumber].position;

                SpawnObject spawned = Instantiate(spawnObjects[i], spawnPointPosition, Quaternion.identity, transform);
                spawned.GetComponent<ObjectMover>().InitSpeedController(speedController);

                if (spawned.TryGetComponent(out Enemy enemy))
                {
                    enemy.InitScoreKeeper(scoreKeeper);
                }

                if (spawned.TryGetComponent(out Coin coin))
                {
                    coin.InitScoreKeeper(scoreKeeper);
                }

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
        float speed = speedController.CurrentSpeed;
        speed = Mathf.Clamp(speed, 1, speedController.SpeedLimit);
        currentSpawnRate = distanceBetweenObjects / speed;

        Debug.Log("Level: " + currentLevel +
                  "   Speed: " + speedController.CurrentSpeed +
                  "   Clamped Speed: " + speed +
                  "   SpawnRate: " + currentSpawnRate);
    }

    public void SetCurrentLevel(int levelValue)
    {
        currentLevel = levelValue;
        LevelChange?.Invoke();
    }

    public void SetSpawnCondition(bool condition)
    {
        isSpawnEnable = condition;
    }
}