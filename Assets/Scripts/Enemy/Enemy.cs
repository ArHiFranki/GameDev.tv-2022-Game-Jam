using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem dieFX;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private PolygonCollider2D enemyCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(damage);
            if (!player.IsDead)
            {
                StartCoroutine(DieCoroutine());
            }
        }
        else
        {
            Die();
        }
    }

    IEnumerator DieCoroutine()
    {
        enemySprite.enabled = false;
        enemyCollider.enabled = false;
        dieFX.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        enemySprite.enabled = true;
        enemyCollider.enabled = true;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
