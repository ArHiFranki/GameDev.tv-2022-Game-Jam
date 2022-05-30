using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip coinUpSound;
    [SerializeField] private AudioClip powerUpSound;
    [SerializeField] private AudioClip healthUpSound;
    [SerializeField] private AudioClip takeDamegeSound;
    [SerializeField] private AudioClip destroyEmenySound;
    [SerializeField] private AudioClip movementSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip onMouseClickUISound;
    [SerializeField] private AudioClip onMouseOverUISound;
    [SerializeField] private AudioClip gameThemeMusic;
    [SerializeField] private AudioClip menuThemeMusic;
    [SerializeField] private AudioClip hellThemeMusicLoop;
    [SerializeField] private AudioClip hellThemeMusicIntro;
    [SerializeField] private AudioClip pickUpShotgunSound;
    [SerializeField] private AudioClip shotgunFireSound;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private SettingsController settingsController;
    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;
    [SerializeField] private float gameOverSoundPitch;
    [SerializeField] private float coinUpVolume;
    [SerializeField] private float powerUpVolume;
    [SerializeField] private float healthUpVolume;
    [SerializeField] private float takeDamageVolume;
    [SerializeField] private float destroyEnemyVolume;
    [SerializeField] private float movementVolume;
    [SerializeField] private float gameOverVolume;
    [SerializeField] private float onMouseClickUIVolume;
    [SerializeField] private float onMouseOverUIVolume;
    [SerializeField] private float pickUpShotgunSoundVolume;
    [SerializeField] private float shotgunFireSoundVolume;

    private AudioSource gameSounds;
    private const string menuScene = "MenuScene";
    private const string gameScene = "GameScene";
    private const string settingsControllerName = "SettingsController";
    private float coinUpVolumeValue;
    private float powerUpVolumeValue;
    private float healthUpVolumeValue;
    private float takeDamageVolumeValue;
    private float destroyEnemyVolumeValue;
    private float movementVolumeValue;
    private float gameOverVolumeValue;
    private float onMouseClickUIVolumeValue;
    private float onMouseOverUIVolumeValue;
    private float pickUpShotgunSoundVolumeValue;
    private float shotgunFireSoundVolumeValue;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == gameScene)
        {
            speedController.SpeedChange += ChangeBackgroundMusicPitch;
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == gameScene)
        {
            speedController.SpeedChange -= ChangeBackgroundMusicPitch;
        }
    }

    private void Awake()
    {
        gameSounds = GetComponent<AudioSource>();
        settingsController = GameObject.Find(settingsControllerName).GetComponent<SettingsController>();
        gameSounds.Stop();

        if (SceneManager.GetActiveScene().name == menuScene)
        {
            PlayBackgroundMusic(menuThemeMusic, settingsController.MusicVolume);
        }
        else if (SceneManager.GetActiveScene().name == gameScene)
        {
            PlayBackgroundMusic(gameThemeMusic, settingsController.MusicVolume);
        }
    }

    private void Start()
    {
        EffectsVolumeCalculator();
    }

    private void PlayBackgroundMusic(AudioClip musicTheme, float volume = 0.5f)
    {
        gameSounds.loop = true;
        gameSounds.clip = musicTheme;
        gameSounds.volume = volume;
        gameSounds.Play();
    }

    private void ChangeBackgroundMusicPitch()
    {
        float pitch;
        float tempSpeed = speedController.CurrentSpeed - speedController.StartSpeed - speedController.SpeedIncrement;
        float interpolationFactor = tempSpeed / speedController.SpeedLimit;
        pitch = Mathf.Lerp(pitchMin, pitchMax, interpolationFactor);
        gameSounds.pitch = pitch;
        Debug.Log("Current Pitch: " + pitch);
    }

    public void EffectsVolumeCalculator()
    {
        if (settingsController.MusicVolume <= 0)
        {
            gameSounds.volume = 0.0001f;
            settingsController.SetMusicVolume(0.0001f);
        }
        coinUpVolumeValue = (coinUpVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        powerUpVolumeValue = (powerUpVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        healthUpVolumeValue = (healthUpVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        takeDamageVolumeValue = (takeDamageVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        destroyEnemyVolumeValue = (destroyEnemyVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        movementVolumeValue = (movementVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        gameOverVolumeValue = (gameOverVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        onMouseClickUIVolumeValue = (onMouseClickUIVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        onMouseOverUIVolumeValue = (onMouseOverUIVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        pickUpShotgunSoundVolumeValue = (pickUpShotgunSoundVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
        shotgunFireSoundVolumeValue = (shotgunFireSoundVolume / settingsController.MusicVolume) * settingsController.EffectsVolume;
    }

    public void PlayCoinUpSound()
    {
        gameSounds.PlayOneShot(coinUpSound, coinUpVolumeValue);
    }

    public void PlayPowerUpSound()
    {
        gameSounds.PlayOneShot(powerUpSound, powerUpVolumeValue);
    }

    public void PlayHealthUpSound()
    {
        gameSounds.PlayOneShot(healthUpSound, healthUpVolumeValue);
    }

    public void PlayTakeDamageSound()
    {
        gameSounds.PlayOneShot(takeDamegeSound, takeDamageVolumeValue);
    }

    public void PlayDestroyEnemySound()
    {
        gameSounds.PlayOneShot(destroyEmenySound, destroyEnemyVolumeValue);
    }

    public void PlayMovementSound()
    {
        gameSounds.PlayOneShot(movementSound, movementVolumeValue);
    }

    public void PlayGameOverSound()
    {
        gameSounds.pitch = gameOverSoundPitch;
        gameSounds.PlayOneShot(gameOverSound, gameOverVolumeValue);
    }

    public void PlayOnMouseClickUISound()
    {
        gameSounds.PlayOneShot(onMouseClickUISound, onMouseClickUIVolumeValue);
    }

    public void PlayOnMouseOverUISound()
    {
        gameSounds.PlayOneShot(onMouseOverUISound, onMouseOverUIVolumeValue);
    }

    public void PlayPickUpShotgunSound()
    {
        gameSounds.PlayOneShot(pickUpShotgunSound, pickUpShotgunSoundVolumeValue);
    }

    public void PlayShotgunFireSound()
    {
        gameSounds.PlayOneShot(shotgunFireSound, shotgunFireSoundVolumeValue);
    }

    public void StopBackgroundMusic()
    {
        gameSounds.Stop();
    }

    public void PlayBackgroundMusic()
    {
        gameSounds.Play();
    }

    public void ChangeBackgroundMusicPitchTo(float pitchValue)
    {
        gameSounds.pitch = pitchValue;
    }

    public void ChangeBackgroundMusicVolumeTo(float musicVolumeValue)
    {
        gameSounds.volume = musicVolumeValue;
    }
}
