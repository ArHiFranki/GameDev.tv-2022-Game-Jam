public class LavaPit : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.Die();
    }
}
