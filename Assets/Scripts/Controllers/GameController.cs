using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject diedCanvas;
    [SerializeField] private GameObject butText;
    [SerializeField] private GameObject beginningText;
    [SerializeField] private GameObject hellCanvas;
    [SerializeField] private float butTextDelay;
    [SerializeField] private float beginningTextDelay;
    [SerializeField] private float transmitToHellDelay;
    [SerializeField] private GameObject worldCleaner;
    [SerializeField] private GameObject firstSpawner;
    [SerializeField] private GameObject hellSpawner;

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
        gameCanvas.SetActive(false);
        diedCanvas.SetActive(true);
        worldCleaner.SetActive(true);
        firstSpawner.SetActive(false);
        yield return new WaitForSeconds(butTextDelay);
        butText.SetActive(true);
        worldCleaner.SetActive(false);
        yield return new WaitForSeconds(beginningTextDelay);
        beginningText.SetActive(true);
        player.SetInitialCondition();
        yield return new WaitForSeconds(transmitToHellDelay);
        diedCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        hellCanvas.SetActive(true);
        hellSpawner.SetActive(true);
        UnfreezeWorld();
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
