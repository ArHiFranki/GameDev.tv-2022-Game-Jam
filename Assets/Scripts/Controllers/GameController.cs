using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnEnable()
    {
        player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        StartCoroutine(TransmitToHell());
    }

    private void OnResurrection()
    {
        StartCoroutine(Resurrection());
    }

    private IEnumerator TransmitToHell()
    {
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Resurrection()
    {
        yield return new WaitForSeconds(1);
    }
}
