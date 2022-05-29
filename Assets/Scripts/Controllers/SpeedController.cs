using UnityEngine;
using UnityEngine.Events;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Spawner firstSpawner;
    [SerializeField] private Spawner hellSpawner;
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float starSpeedMultiplier;
    [SerializeField] private float speedLimit;

    private float currentSpeed;

    public float CurrentSpeed => currentSpeed;
    public float SpeedLimit => speedLimit;
    //public float StartSpeed => startSpeed;
    //public float SpeedIncrement => speedIncrement;

    public event UnityAction SpeedChange;

    private void OnEnable()
    {
        firstSpawner.LevelChange += OnSpeedChange;
        hellSpawner.LevelChange += OnSpeedChange;
        player.PowerUpStatusChanged += OnSpeedChange;
    }

    private void OnDisable()
    {
        firstSpawner.LevelChange -= OnSpeedChange;
        hellSpawner.LevelChange -= OnSpeedChange;
        player.PowerUpStatusChanged -= OnSpeedChange;
    }

    private void OnSpeedChange()
    {
        if (player.IsInHell)
        {
            SpeedChangeCalculator(hellSpawner.CurrentLevel);
        }
        else
        {
            SpeedChangeCalculator(firstSpawner.CurrentLevel);
        }
    }

    private void SpeedChangeCalculator(int currentLevel)
    {
        float tmpCurrentSpeed;

        if (player.IsPowerUp)
        {
            tmpCurrentSpeed = (startSpeed + currentLevel * speedIncrement) * starSpeedMultiplier;
        }
        else
        {
            tmpCurrentSpeed = startSpeed + currentLevel * speedIncrement;
        }

        if (tmpCurrentSpeed >= speedLimit)
        {
            tmpCurrentSpeed = speedLimit;
        }

        currentSpeed = tmpCurrentSpeed;
        SpeedChange?.Invoke();
    }

    public void SetCurrentSpeed(float speedValue)
    {
        currentSpeed = speedValue;
        SpeedChange?.Invoke();
    }
}
