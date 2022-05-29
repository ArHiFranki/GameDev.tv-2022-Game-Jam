using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int startHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private ParticleSystem starHitFX;
    [SerializeField] private ParticleSystem powerUpWindFX;
    [SerializeField] private ParticleSystem playerDieFX;
    [SerializeField] private SoundController soundController;
    [SerializeField] private SpawnObject middleShotgunPrefab;
    [SerializeField] private SpawnObject lavaPitPrefab;
    [SerializeField] private SpeedController speedController;

    private const string powerUpAnimationTrigger = "isPowerUp";
    private const string moveAnimationSpeed = "moveSpeed";
    private const string takeDamageAnimationTrigger = "TakeDamage";
    private int currentHealth;
    private float endPowerUpTime;
    private bool isPowerUp;
    private bool hasWeapon;
    private bool isDead;
    private Animator playerAnimator;

    public bool IsPowerUp => isPowerUp;
    public bool HasWeapon => hasWeapon;
    public bool IsDead => isDead;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;
    public event UnityAction AliveStatusChanged;
    public event UnityAction PowerUpStatusChanged;
    public event UnityAction WeaponStatusChanged;
    //public event UnityAction PlayerTakeDamage;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetInitialCondition();
    }

    private void FixedUpdate()
    {
        // ToDo: ��������� ������ �������, �������� � ��������� �����
        if (isPowerUp && (Time.time >= endPowerUpTime))
        {
            isPowerUp = false;
            PowerUpStatusChanged?.Invoke();
            playerAnimator.SetBool(powerUpAnimationTrigger, false);
            playerAnimator.SetFloat(moveAnimationSpeed, 1f);
            powerUpWindFX.Stop();
        }
    }

    private void SetInitialCondition()
    {
        currentHealth = startHealth;
        isDead = false;
        AliveStatusChanged?.Invoke();
        isPowerUp = false;
        PowerUpStatusChanged?.Invoke();
        HealthChanged?.Invoke(currentHealth);
        powerUpWindFX.Stop();
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
        Debug.Log("You Die");
        isDead = true;
        DisableWeapon();
        AliveStatusChanged?.Invoke();
        StartCoroutine(PlayerDieCoroutine());
    }

    private IEnumerator PlayerDieCoroutine()
    {
        soundController.StopBackgroundMusic();
        playerDieFX.Play();
        yield return new WaitForSeconds(playerDieFX.main.duration);
        Died?.Invoke();
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

    public void SetWinCondition()
    {
        Debug.Log("You Win!");
    }

    public void EnableWeapon()
    {
        Debug.Log("EnableWeapon");
        hasWeapon = true;
        WeaponStatusChanged?.Invoke();
    }

    public void DisableWeapon()
    {
        Debug.Log("DisableWeapon");
        hasWeapon = false;
        WeaponStatusChanged?.Invoke();
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
}
