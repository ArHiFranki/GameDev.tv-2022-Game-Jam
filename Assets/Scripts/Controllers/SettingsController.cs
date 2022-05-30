using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectsVolume;
    [SerializeField] private float defaultMusicVolume;
    [SerializeField] private float defaultEffectsVolume;

    public float MusicVolume => musicVolume;
    public float EffectsVolume => effectsVolume;

    private void Awake()
    {
        //int objectsCount = FindObjectsOfType<SettingsController>().Length;

        //if (objectsCount > 1)
        //{
        //    gameObject.SetActive(false);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
    }

    public void SetEffectsVolume(float value)
    {
        effectsVolume = value;
    }
}
