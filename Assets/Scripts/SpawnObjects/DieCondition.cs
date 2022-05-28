public class DieCondition : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.Die();
    }
}
