public class Shotgun : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.EnableWeapon();
        if (player.IsInHell)
        {
            player.PickUpshotgunInHellEventTrigger();
        }
    }
}
