using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer shotgunSpriteRenderer;
    [SerializeField] private SpriteRenderer handSpriteRenderer;
    [SerializeField] private Sprite playerNormalSprite;
    [SerializeField] private Sprite playerNormalFireSprite;
    [SerializeField] private Sprite playerNormalHand;
    [SerializeField] private Sprite playerPowerUpSprite;
    [SerializeField] private Sprite playerPowerUpFireSprite;
    [SerializeField] private Sprite playerPowerUpHand;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.PowerUp += UpdatePlayerSprite;
        player.PowerDown += UpdatePlayerSprite;
        player.WeaponOn += UpdatePlayerSprite;
        player.WeaponOff += UpdatePlayerSprite;
    }

    private void OnDisable()
    {
        player.PowerUp -= UpdatePlayerSprite;
        player.PowerDown -= UpdatePlayerSprite;
        player.WeaponOn -= UpdatePlayerSprite;
        player.WeaponOff -= UpdatePlayerSprite;
    }

    private void Start()
    {
        UpdatePlayerSprite();
    }

    private void UpdatePlayerSprite()
    {
        if (player.IsPowerUp && player.HasWeapon)
        {
            playerSpriteRenderer.sprite = playerPowerUpFireSprite;
            handSpriteRenderer.enabled = true;
            handSpriteRenderer.sprite = playerPowerUpHand;
            shotgunSpriteRenderer.enabled = true;
        }
        else if (player.IsPowerUp && !player.HasWeapon)
        {
            playerSpriteRenderer.sprite = playerPowerUpSprite;
            handSpriteRenderer.enabled = false;
            shotgunSpriteRenderer.enabled = false;
        }
        else if (!player.IsPowerUp && player.HasWeapon)
        {
            playerSpriteRenderer.sprite = playerNormalFireSprite;
            handSpriteRenderer.enabled = true;
            handSpriteRenderer.sprite = playerNormalHand;
            shotgunSpriteRenderer.enabled = true;
        }
        else if (!player.IsPowerUp && !player.HasWeapon)
        {
            playerSpriteRenderer.sprite = playerNormalSprite;
            handSpriteRenderer.enabled = false;
            shotgunSpriteRenderer.enabled = false;
        }
    }
}
