public class MiddleShotgunTrigger : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.SpawnShotgun();
    }
}
