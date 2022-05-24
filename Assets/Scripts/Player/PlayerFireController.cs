using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float firstBulletRotation;
    [SerializeField] private float secondBulletRotation;

    private float timeAfterLastShoot;
    private float tmpAngle;

    private void Start()
    {
        timeAfterLastShoot = 0;
    }

    private void Update()
    {
        timeAfterLastShoot += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeAfterLastShoot >= fireRate)
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
        }
    }
}
