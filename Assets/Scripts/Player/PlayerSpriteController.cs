using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer shotgunSpriteRenderer;
    [SerializeField] private SpriteRenderer handSpriteRenderer;
    [SerializeField] private SpriteRenderer mouthSpriteRenderer;
    [SerializeField] private SpriteRenderer eyesSpriteRenderer;
    [SerializeField] private SpriteRenderer shadowSpriteRenderer;
    [SerializeField] private Sprite playerNormalSprite;
    [SerializeField] private Sprite playerNormalFireSprite;
    [SerializeField] private Sprite playerNormalHand;
    [SerializeField] private Sprite playerPowerUpSprite;
    [SerializeField] private Sprite playerPowerUpFireSprite;
    [SerializeField] private Sprite playerPowerUpHand;
    [SerializeField] private PolygonCollider2D playerCollider;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.PowerUpStatusChanged += OnStatusChanged;
        player.WeaponStatusChanged += OnStatusChanged;
        player.AliveStatusChanged += OnAliveStatusChanged;
    }

    private void OnDisable()
    {
        player.PowerUpStatusChanged -= OnStatusChanged;
        player.WeaponStatusChanged -= OnStatusChanged;
        player.AliveStatusChanged -= OnAliveStatusChanged;
    }

    private void Start()
    {
        OnAliveStatusChanged();
        OnStatusChanged();
    }

    private void OnStatusChanged()
    {
        if (player.IsPowerUp && player.HasWeapon)
        {
            playerSpriteRenderer.enabled = true;
            playerSpriteRenderer.sprite = playerPowerUpFireSprite;
            handSpriteRenderer.enabled = true;
            handSpriteRenderer.sprite = playerPowerUpHand;
            shotgunSpriteRenderer.enabled = true;
        }
        else if (player.IsPowerUp && !player.HasWeapon)
        {
            playerSpriteRenderer.enabled = true;
            playerSpriteRenderer.sprite = playerPowerUpSprite;
            handSpriteRenderer.enabled = false;
            shotgunSpriteRenderer.enabled = false;
        }
        else if (!player.IsPowerUp && player.HasWeapon)
        {
            playerSpriteRenderer.enabled = true;
            playerSpriteRenderer.sprite = playerNormalFireSprite;
            handSpriteRenderer.enabled = true;
            handSpriteRenderer.sprite = playerNormalHand;
            shotgunSpriteRenderer.enabled = true;
        }
        else if (!player.IsPowerUp && !player.HasWeapon)
        {
            playerSpriteRenderer.enabled = true;
            playerSpriteRenderer.sprite = playerNormalSprite;
            handSpriteRenderer.enabled = false;
            shotgunSpriteRenderer.enabled = false;
        }
    }

    private void OnAliveStatusChanged()
    {
        if (player.IsDead)
        {
            playerSpriteRenderer.enabled = false;
            shotgunSpriteRenderer.enabled = false;
            handSpriteRenderer.enabled = false;
            mouthSpriteRenderer.enabled = false;
            eyesSpriteRenderer.enabled = false;
            shadowSpriteRenderer.enabled = false;
            playerCollider.enabled = false;
        }
        else
        {
            playerSpriteRenderer.enabled = true;
            shotgunSpriteRenderer.enabled = true;
            handSpriteRenderer.enabled = true;
            mouthSpriteRenderer.enabled = true;
            eyesSpriteRenderer.enabled = true;
            shadowSpriteRenderer.enabled = true;
            playerCollider.enabled = true;
        }
    }
}
