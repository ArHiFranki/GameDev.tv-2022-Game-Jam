using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpeedController speedController;

    private float tmpSpeed;

    private void OnEnable()
    {
        player.Died += OnPlayerDied;
        player.FreezeWorld += OnFreezeWorld;
    }

    private void OnDisable()
    {
        player.Died -= OnPlayerDied;
        player.FreezeWorld -= OnFreezeWorld;
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
        Debug.Log("Tarsmin to Hell");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Resurrection()
    {
        yield return new WaitForSeconds(1);
    }

    private void OnFreezeWorld()
    {
        tmpSpeed = speedController.CurrentSpeed;
        speedController.SetCurrentSpeed(0);
    }

    private void UnfreezeWorld()
    {
        speedController.SetCurrentSpeed(tmpSpeed);
    }
}
