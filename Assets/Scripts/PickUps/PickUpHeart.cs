using UnityEngine;

public class PickUpHeart : PickUp
{
    [SerializeField] private int hpValue;

    protected override void PickUpAction(Player player)
    {
        player.HealthUp(hpValue);
    }
}
