using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button restartButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
