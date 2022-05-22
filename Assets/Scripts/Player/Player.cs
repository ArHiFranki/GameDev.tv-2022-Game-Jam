using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int coinCount = 0;
    [SerializeField] private bool isPowerUp;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite powerUpSprite;
    [SerializeField] ParticleSystem starHitFX;
    [SerializeField] ParticleSystem powerUpWindFX;
    [SerializeField] SoundController soundController;

    private const string powerUpAnimationTrigger = "isPowerUp";
    private const string moveAnimationSpeed = "moveSpeed";
    private int currentHealth;
    private bool isDead;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    private float endPowerUpTime;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> CoinChanged;
    public event UnityAction Died;
    public event UnityAction PowerUp;
    public event UnityAction PowerDown;
    public int Health => currentHealth;
    public int MaxHealth => maxHealth;
    public int CoinCount => coinCount;
    public bool IsPowerUp => isPowerUp;
    public bool IsDead => isDead;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            PowerDown?.Invoke();
            spriteRenderer.sprite = defaultSprite;
            playerAnimator.SetBool(powerUpAnimationTrigger, false);
            playerAnimator.SetFloat(moveAnimationSpeed, 1f);
            powerUpWindFX.Stop();
        }
    }

    private void SetInitialCondition()
    {
        currentHealth = maxHealth;
        isPowerUp = false;
        isDead = false;
        spriteRenderer.sprite = defaultSprite;
        HealthChanged?.Invoke(currentHealth);
        powerUpWindFX.Stop();
    }

    public void ApplyDamage(int damage)
    {
        if (!isPowerUp)
        {
            currentHealth -= damage;
            HealthChanged?.Invoke(currentHealth);
            soundController.PlayTakeDamageSound();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            starHitFX.Play();
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
        isDead = true;
        Died?.Invoke();
        soundController.StopBackgroundMusic();
        soundController.PlayGameOverSound();
    }

    public void ChangeCoinCount(int coinValue)
    {
        coinCount += coinValue;
        CoinChanged?.Invoke(coinCount);
        soundController.PlayCoinUpSound();
    }

    public void GetPowerUp(float powerUpDuration)
    {
        endPowerUpTime = Time.time + powerUpDuration;
        isPowerUp = true;
        PowerUp?.Invoke();
        spriteRenderer.sprite = powerUpSprite;
        playerAnimator.SetBool(powerUpAnimationTrigger, true);
        playerAnimator.SetFloat(moveAnimationSpeed, 1.5f);
        powerUpWindFX.Play();
        soundController.PlayPowerUpSound();
    }
}
