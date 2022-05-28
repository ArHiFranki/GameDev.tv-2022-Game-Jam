using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PickUp
{
    protected override void PickUpAction(Player player)
    {
        player.EnableWeapon();
    }
}
