using UnityEngine;
using UnityEngine.Events;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float starSpeedMultiplier;
    [SerializeField] private float speedLimit;

    private float currentSpeed;

    public float CurrentSpeed => currentSpeed;
    public float SpeedLimit => speedLimit;
    public float StartSpeed => startSpeed;
    public float SpeedIncrement => speedIncrement;

    public event UnityAction SpeedChange;

    private void OnEnable()
    {
        spawner.LevelChange += OnSpeedChange;
        player.PowerUpStatusChanged += OnSpeedChange;
    }

    private void OnDisable()
    {
        spawner.LevelChange -= OnSpeedChange;
        player.PowerUpStatusChanged -= OnSpeedChange;
    }

    private void OnSpeedChange()
    {
        float tmpCurrentSpeed;

        if (player.IsPowerUp)
        {
            tmpCurrentSpeed = (startSpeed + spawner.CurrentLevel * speedIncrement) * starSpeedMultiplier;
        }
        else
        {
            tmpCurrentSpeed = startSpeed + spawner.CurrentLevel * speedIncrement;
        }

        if (tmpCurrentSpeed >= speedLimit)
        {
            tmpCurrentSpeed = speedLimit;
        }

        currentSpeed = tmpCurrentSpeed;
        SpeedChange?.Invoke();
    }
}
