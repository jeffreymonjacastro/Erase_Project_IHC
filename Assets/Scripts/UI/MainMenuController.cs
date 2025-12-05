using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [Tooltip("Scene to load when pressing Play (intro cinematic). Leave empty to go straight to gameplay.")]
    [SerializeField] private string introSceneName = "IntroScene";

    [Tooltip("Fallback scene if introSceneName is empty or you want to skip intro for debugging.")]
    [SerializeField] private string gameplaySceneName = "GameScene";

    [Tooltip("If true, Play goes directly to gameplaySceneName, skipping intro.")]
    [SerializeField] private bool skipIntro = false;

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
        if (mainPanel == null)
        {
            Debug.LogError("[MainMenuController] Missing reference: main panel");
        }

        if (settingsPanel == null)
        {
            Debug.LogError("[MainMenuController] Missing reference: settings panel");
        }

        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // Called by Play button
    public void OnPlayPressed()
    {
        string targetScene;

        if (skipIntro || string.IsNullOrEmpty(introSceneName))
        {
            targetScene = gameplaySceneName;
        }
        else
        {
            targetScene = introSceneName;
        }

        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogError("[MainMenuController] No target scene configured for Play.");
        }
    }

    // Called by Settings button
    public void OnSettingsPressed()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Called by Back button inside Settings
    public void OnSettingsBackPressed()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Called by Exit button
    public void OnExitPressed()
    {
        Application.Quit();

        // So it works in the editor too
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}