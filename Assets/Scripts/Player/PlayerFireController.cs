using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float deltaRotation;

    private float timeAfterLastShoot;

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
            float tmpAngle = firePoint.rotation.eulerAngles.z - deltaRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            tmpAngle = firePoint.rotation.eulerAngles.z + deltaRotation;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tmpAngle));
            timeAfterLastShoot = 0;
        }
    }
}
