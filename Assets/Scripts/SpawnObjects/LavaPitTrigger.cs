public class LavaPitTrigger : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.SpawnLavaPit();
    }
}
