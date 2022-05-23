using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected SpeedController speedController;

    [SerializeField] private Transform enemySlimeContainer;
    [SerializeField] private Transform enemyDinoContainer;
    [SerializeField] private Transform coinContainer;
    [SerializeField] private Transform pickUpHeartContainer;
    [SerializeField] private Transform starContainer;

    [SerializeField] private SpawnObject enemySlimePrefab;
    [SerializeField] private SpawnObject enemyDinoPrefab;
    [SerializeField] private SpawnObject coinPrefab;
    [SerializeField] private SpawnObject pickUpHeartPrefab;
    [SerializeField] private SpawnObject starPrefab;

    [SerializeField] private int enemySlimePoolCapacity;
    [SerializeField] private int enemyDinoPoolCapacity;
    [SerializeField] private int coinPoolCapacity;
    [SerializeField] private int pickUpHeartPoolCapacity;
    [SerializeField] private int starPoolCapacity;

    protected List<SpawnObject> enemyOnePool = new List<SpawnObject>();
    protected List<SpawnObject> enemyTwoPool = new List<SpawnObject>();
    protected List<SpawnObject> coinPool = new List<SpawnObject>();
    protected List<SpawnObject> pickUpHeartPool = new List<SpawnObject>();
    protected List<SpawnObject> starPool = new List<SpawnObject>();

    private void Initialize(SpawnObject prefab, List<SpawnObject> pool, Transform container, int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            SpawnObject spawned = Instantiate(prefab, container.transform);
            spawned.gameObject.SetActive(false);
            spawned.GetComponent<ObjectMover>().InitSpeedController(speedController);
            pool.Add(spawned);
        }
    }

    protected void InitPools()
    {
        Initialize(enemySlimePrefab, enemyOnePool, enemySlimeContainer, enemySlimePoolCapacity);
        Initialize(enemyDinoPrefab, enemyTwoPool, enemyDinoContainer, enemyDinoPoolCapacity);
        Initialize(starPrefab, starPool, starContainer, starPoolCapacity);
        Initialize(pickUpHeartPrefab, pickUpHeartPool, pickUpHeartContainer, pickUpHeartPoolCapacity);
        Initialize(coinPrefab, coinPool, coinContainer, coinPoolCapacity);
    }

    protected bool TryGetObject(List<SpawnObject> pool, out SpawnObject result)
    {
        result = pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
