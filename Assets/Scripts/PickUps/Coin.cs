using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] private int coinValue;

    protected override void PickUpAction(Player player)
    {
        player.ChangeCoinCount(coinValue);
    }
}
