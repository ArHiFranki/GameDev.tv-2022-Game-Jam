using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpeedController speedController;
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
    [SerializeField] private PlayerMoveController playerMoveController;
    [SerializeField] private float newMaxHeight;
    [SerializeField] private float newMinHeight;
    [SerializeField] private float newMaxWidth;
    [SerializeField] private float newMinWidth;
    [SerializeField] private GameObject shootText;
    [SerializeField] private GameObject shootTextShadow;
    [SerializeField] private float shootTextDelay;
    [SerializeField] private int shootTextBlinkCount;
    [SerializeField] private GameObject gameOverScreen;

    private float tmpSpeed;
    private bool isFirstShotgunPickUp = true;

    private void OnEnable()
    {
        player.Died += OnPlayerDied;
        player.FreezeWorld += OnFreezeWorld;
        player.PickUpShotgunInHell += OnPickUpShotgunInHell;
        player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        player.Died -= OnPlayerDied;
        player.FreezeWorld -= OnFreezeWorld;
        player.PickUpShotgunInHell -= OnPickUpShotgunInHell;
        player.GameOver -= OnGameOver;
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
        diedCanvas.SetActive(true);
        worldCleaner.SetActive(true);
        firstSpawner.SetActive(false);
        yield return new WaitForSeconds(butTextDelay);
        butText.SetActive(true);
        worldCleaner.SetActive(false);
        yield return new WaitForSeconds(beginningTextDelay);
        beginningText.SetActive(true);
        player.SetInitialCondition();
        player.SetHealthValue(1);
        playerMoveController.SetBorders(newMaxHeight, newMinHeight, newMaxWidth, newMinWidth);
        yield return new WaitForSeconds(transmitToHellDelay);
        diedCanvas.SetActive(false);
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

    private void OnPickUpShotgunInHell()
    {
        Debug.Log("PickUp Shotgun in Hell");
        playerMoveController.ResetBorders();
        if(isFirstShotgunPickUp)
        {
            StartCoroutine(BlinkShootText());
            isFirstShotgunPickUp = false;
        }
    }

    private IEnumerator BlinkShootText()
    {
        for (int i = 0; i < shootTextBlinkCount; i++)
        {
            shootText.SetActive(true);
            shootTextShadow.SetActive(true);
            yield return new WaitForSeconds(shootTextDelay);
            shootText.SetActive(false);
            shootTextShadow.SetActive(false);
            yield return new WaitForSeconds(shootTextDelay);
        }
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over");
        hellSpawner.GetComponent<Spawner>().SetSpawnCondition(false);
        gameOverScreen.SetActive(true);
    }
}
