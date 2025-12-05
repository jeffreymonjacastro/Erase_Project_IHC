using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroSceneController : MonoBehaviour
{
    [Header("Scene Flow")]
    [Tooltip("Scene to load after the intro video (main gameplay scene).")]
    [SerializeField] private string nextSceneName = "GameScene";

    [Header("References")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Tooltip("Optional: fade out duration in seconds before changing scene.")]
    [SerializeField] private float fadeOutDuration = 0.0f;

    private bool hasFinishedOrSkipped = false;

    private void Awake()
    {
        if (videoPlayer != null)
        {
            // Called when the video reaches its end
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("[IntroSceneController] VideoPlayer not assigned.");
        }
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (!hasFinishedOrSkipped)
        {
            hasFinishedOrSkipped = true;
            LoadNextScene();
        }
    }

    // Called by the Skip button
    public void OnSkipPressed()
    {
        if (!hasFinishedOrSkipped)
        {
            hasFinishedOrSkipped = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("[IntroSceneController] nextSceneName is empty, cannot load scene.");
            return;
        }

        // For now, no fade: just load
        SceneManager.LoadScene(nextSceneName);
    }

    private void Update()
    {
        if (!hasFinishedOrSkipped)
        {
            // B button pressed
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                hasFinishedOrSkipped = true;
                LoadNextScene();
            }
        }
    }
}
