using UnityEngine;

public class Star : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.GetPowerUp();
    }
}
