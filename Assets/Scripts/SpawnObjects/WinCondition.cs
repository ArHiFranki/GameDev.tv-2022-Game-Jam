public class WinCondition : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.SetWinCondition();
    }
}
