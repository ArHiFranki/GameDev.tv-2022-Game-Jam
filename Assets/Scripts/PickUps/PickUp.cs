using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PickUpAction(player);
        }

        Die();
    }

    protected abstract void PickUpAction(Player player);

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
