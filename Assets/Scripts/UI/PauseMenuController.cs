using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseCanvas;

    [Header("Scene Navigation")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Behaviour")]
    [SerializeField] private bool useTimeScalePause = true;

    private bool isPaused = false;

    public bool IsPaused => isPaused;

    private void Awake()
    {
        if (pauseCanvas == null)
        {
            Debug.LogError("[PauseMenuController] Missing reference: pause canvas");
        }

        if (string.IsNullOrEmpty(mainMenuSceneName))
        {
            Debug.LogError("[PauseMenuController] Missing reference: main menu scene");
        }
    }

    private void Start()
    {
        pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isPaused) return;
        isPaused = true;

        pauseCanvas.SetActive(true);

        if (useTimeScalePause)
        {
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (!isPaused) return;
        isPaused = false;

        pauseCanvas.SetActive(false);

        if (useTimeScalePause)
        {
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        // Make sure the game isn't stuck paused in the menu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void OnDestroy()
    {
        // Safety net: never leave the timescale frozen
        Time.timeScale = 1f;
    }
}
