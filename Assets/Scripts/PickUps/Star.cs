using UnityEngine;

public class Star : PickUp
{
    [SerializeField] private float puwerUpDuration;

    protected override void PickUpAction(Player player)
    {
        player.GetPowerUp(puwerUpDuration);
    }
}
