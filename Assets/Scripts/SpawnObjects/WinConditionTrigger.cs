public class WinConditionTrigger : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.SpawnCastle();
    }
}
