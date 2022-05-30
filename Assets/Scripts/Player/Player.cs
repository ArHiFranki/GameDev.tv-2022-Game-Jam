using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMoveController))]
public class Player : MonoBehaviour
{
    [SerializeField] private int startHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxAmmo;
    [SerializeField] private ParticleSystem starHitFX;
    [SerializeField] private ParticleSystem powerUpWindFX;
    [SerializeField] private ParticleSystem playerDieFX;
    [SerializeField] private SoundController soundController;
    [SerializeField] private SpawnObject middleShotgunPrefab;
    [SerializeField] private SpawnObject lavaPitPrefab;
    [SerializeField] private SpawnObject castlePrefab;
    [SerializeField] private SpeedController speedController;

    private const string powerUpAnimationTrigger = "isPowerUp";
    private const string moveAnimationSpeed = "moveSpeed";
    private const string takeDamageAnimationTrigger = "TakeDamage";
    private int currentHealth;
    private int currentAmmo;
    private float endPowerUpTime;
    private bool isPowerUp;
    private bool hasWeapon;
    private bool isDead;
    private bool isInHell;
    private bool isGameOver;
    private Animator playerAnimator;
    private PlayerMoveController moveController;
    private Vector3 startPosition;

    public bool IsPowerUp => isPowerUp;
    public bool HasWeapon => hasWeapon;
    public bool IsDead => isDead;
    public bool IsInHell => isInHell;
    public bool IsGameOver => isGameOver;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> AmmoChanged;
    public event UnityAction Died;
    public event UnityAction AliveStatusChanged;
    public event UnityAction PowerUpStatusChanged;
    public event UnityAction WeaponStatusChanged;
    public event UnityAction FreezeWorld;
    public event UnityAction PickUpShotgunInHell;
    public event UnityAction GameOver;
    public event UnityAction PlayerEnterCastleTrigger;
    public event UnityAction PlayerWin;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        moveController = GetComponent<PlayerMoveController>();
    }

    private void Start()
    {
        startPosition = transform.position;
        isInHell = false;
        isGameOver = false;
        SetInitialCondition();
    }

    private void FixedUpdate()
    {
        if (isPowerUp && (Time.time >= endPowerUpTime))
        {
            TurnOffPowerUp();
        }
    }

    public void SetInitialCondition()
    {
        currentHealth = startHealth;
        isDead = false;
        AliveStatusChanged?.Invoke();
        isPowerUp = false;
        PowerUpStatusChanged?.Invoke();
        HealthChanged?.Invoke(currentHealth);
        powerUpWindFX.Stop();
        transform.position = startPosition;
    }

    public void SetHealthValue(int healthValue)
    {
        currentHealth = healthValue;
        HealthChanged?.Invoke(currentHealth);
    }

    public void ApplyDamage(int damage)
    {
        if (!isPowerUp)
        {
            currentHealth -= damage;
            HealthChanged?.Invoke(currentHealth);
            playerAnimator.SetTrigger(takeDamageAnimationTrigger);
            soundController.PlayTakeDamageSound();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            starHitFX.Play();
            playerAnimator.SetTrigger(takeDamageAnimationTrigger);
            soundController.PlayDestroyEnemySound();
        }
    }

    public void HealthUp(int hpValue)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += hpValue;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            HealthChanged?.Invoke(currentHealth);
            soundController.PlayHealthUpSound();
        }
    }

    public void Die()
    {
        StartCoroutine(PlayerDieCoroutine());
    }

    private IEnumerator PlayerDieCoroutine()
    {
        moveController.DisableMove();
        isDead = true;
        DisableWeapon();
        AliveStatusChanged?.Invoke();
        currentAmmo = 0;
        AmmoChanged?.Invoke(currentAmmo);
        FreezeWorld?.Invoke();
        soundController.StopBackgroundMusic();
        soundController.PlayGameOverSound();
        playerDieFX.Play();
        yield return new WaitForSeconds(playerDieFX.main.duration);

        if (isInHell)
        {
            GameOver?.Invoke();
        }
        else
        {
            Died?.Invoke();
            isInHell = true;
        }
    }

    public void PickUpCoin()
    {
        soundController.PlayCoinUpSound();
    }

    public void GetPowerUp(float powerUpDuration)
    {
        endPowerUpTime = Time.time + powerUpDuration;
        isPowerUp = true;
        PowerUpStatusChanged?.Invoke();
        playerAnimator.SetBool(powerUpAnimationTrigger, true);
        playerAnimator.SetFloat(moveAnimationSpeed, 1.5f);
        powerUpWindFX.Play();
        soundController.PlayPowerUpSound();
    }

    public void EnableWeapon()
    {
        hasWeapon = true;
        WeaponStatusChanged?.Invoke();
        currentAmmo = maxAmmo;
        AmmoChanged?.Invoke(currentAmmo);
        soundController.PlayPickUpShotgunSound();
    }

    public void DisableWeapon()
    {
        hasWeapon = false;
        WeaponStatusChanged?.Invoke();
    }

    public void ReduceAmmo()
    {
        currentAmmo--;
        AmmoChanged?.Invoke(currentAmmo);

        if (currentAmmo <= 0)
        {
            DisableWeapon();
        }

        soundController.PlayShotgunFireSound();
    }

    public void SpawnCastle()
    {
        Debug.Log("Spawn Castle Trigger");
        SpawnObject spawned = Instantiate(castlePrefab);
        spawned.GetComponent<Castle>().InitSpeedController(speedController);
        PlayerEnterCastleTrigger?.Invoke();
    }

    public void SpawnLavaPit()
    {
        Debug.Log("Spawn Die Trigger");
        SpawnObjectAtTheMiddle(lavaPitPrefab);
    }

    public void SpawnShotgun()
    {
        Debug.Log("Spawn Shotgun Trigger");
        SpawnObjectAtTheMiddle(middleShotgunPrefab);
    }

    private void SpawnObjectAtTheMiddle(SpawnObject spawnObject)
    {
        SpawnObject spawned = Instantiate(spawnObject);
        spawned.GetComponent<ObjectMover>().InitSpeedController(speedController);
    }

    public void PickUpshotgunInHellEventTrigger()
    {
        PickUpShotgunInHell?.Invoke();
    }

    public void TurnOffPowerUp()
    {
        isPowerUp = false;
        PowerUpStatusChanged?.Invoke();
        playerAnimator.SetBool(powerUpAnimationTrigger, false);
        playerAnimator.SetFloat(moveAnimationSpeed, 1f);
        powerUpWindFX.Stop();
    }

    public void TriggerWinEvent()
    {
        PlayerWin?.Invoke();
    }
}
