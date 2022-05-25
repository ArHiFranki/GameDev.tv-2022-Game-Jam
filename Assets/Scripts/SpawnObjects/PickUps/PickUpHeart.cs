using UnityEngine;

public class PickUpHeart : PickUp
{
    [SerializeField] private int hpRecoverValue;

    protected override void PickUpAction(Player player)
    {
        player.HealthUp(hpRecoverValue);
    }
}
