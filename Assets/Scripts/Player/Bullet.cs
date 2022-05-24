using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;

    private void OnEnable()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            DestroyBullet();
        }
    }

    private void MoveBullet()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
