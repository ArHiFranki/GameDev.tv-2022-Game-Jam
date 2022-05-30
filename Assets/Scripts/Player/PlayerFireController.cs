using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ParticleSystem fireFX;
    [SerializeField] private float fireRate;
    [SerializeField] private float firstBulletRotation;
    [SerializeField] private float secondBulletRotation;
    [SerializeField] private Animator playerAnimator;

    private float timeAfterLastShoot;
    private float tmpAngle;
    private Player player;
    private bool enableFire;

    private const string fireAnimationTrigger = "Fire";

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        timeAfterLastShoot = 0;
        enableFire = true;
    }

    private void Update()
    {
        timeAfterLastShoot += Time.deltaTime;
    }

    public void Fire()
    {
        if (player.HasWeapon && timeAfterLastShoot >= fireRate && enableFire)
        {
            tmpAngle = firePoint.rotation.eulerAngles.z - firstBulletRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));
            tmpAngle = firePoint.rotation.eulerAngles.z + firstBulletRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));
            tmpAngle = firePoint.rotation.eulerAngles.z - secondBulletRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));
            tmpAngle = firePoint.rotation.eulerAngles.z + secondBulletRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));

            timeAfterLastShoot = 0;
            playerAnimator.SetTrigger(fireAnimationTrigger);
            fireFX.Play();
            player.ReduceAmmo();
        }
    }

    public void DisableFire()
    {
        enableFire = false;
    }
}
