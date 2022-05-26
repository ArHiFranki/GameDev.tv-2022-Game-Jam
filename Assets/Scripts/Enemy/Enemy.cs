using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int reward;
    [SerializeField] private ParticleSystem dieFX;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private PolygonCollider2D enemyCollider;

    private ScoreKeeper myScoreKeeper;
    private bool isRewardReceived = false;

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
        else if (collision.TryGetComponent(out ObjectDestroyer objectDestroyer))
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            StartCoroutine(DieCoroutine());

            if(!isRewardReceived)
            {
                myScoreKeeper.ModifyCoinCount(reward);
                isRewardReceived = true;
            }
        }
    }

    IEnumerator DieCoroutine()
    {
        enemySprite.enabled = false;
        enemyCollider.enabled = false;
        dieFX.Play();
        yield return new WaitForSeconds(dieFX.main.duration);
        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void InitScoreKeeper(ScoreKeeper scoreKeeper)
    {
        myScoreKeeper = scoreKeeper;
    }
}
