using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private float waveDelay;
    [SerializeField] private List<WaveAction> objectsToSpawn;

    [System.Serializable]
    private class WaveAction
    {
        public SpawnObject objectPrefab;
        public int objectCount;
    }
}
