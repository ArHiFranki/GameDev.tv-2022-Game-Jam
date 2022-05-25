using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJumpController : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Jump()
    {
        //if (!player.IsJumping)
        //{
        //    player.PlayerJump();
        //}

        player.PlayerJump();
    }
}
