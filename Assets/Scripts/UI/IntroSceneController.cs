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

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeInDuration = 1.0f;
    [SerializeField] private float fadeOutDuration = 1.0f;

    private bool hasFinishedOrSkipped = false;

    private void Awake()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.playOnAwake = false;
        }
        else
        {
            Debug.LogError("[IntroSceneController]  Missing reference: video player");
        }

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
        }
        else
        {
            Debug.LogError("[IntroSceneController] Missing reference: fade canvas group");
        }

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("[IntroSceneController] nextSceneName is empty.");
        }
    }
    private void Start()
    {
        StartCoroutine(PrepareAndFadeIn());
    }
    private System.Collections.IEnumerator PrepareAndFadeIn()
    {
        videoPlayer.Prepare();

        // Wait until the first frame is ready and shown on the screen
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        // Now the first frame is visible behind the black panel.
        // Fade from black to transparent.
        if (fadeInDuration > 0f)
        {
            yield return Fade(1f, 0f, fadeInDuration);
        }
        else
        {
            fadeCanvasGroup.alpha = 0f;
        }

        // Start video playback AFTER fade-in is done
        if (videoPlayer != null)
        {
            videoPlayer.Play();
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
            StartCoroutine(FadeOutAndLoad(pauseBeforeFade: false));
        }
    }

    public void OnSkipPressed()
    {
        if (!hasFinishedOrSkipped)
        {
            hasFinishedOrSkipped = true;
            StartCoroutine(FadeOutAndLoad(pauseBeforeFade: true));
        }
    }

    private System.Collections.IEnumerator FadeOutAndLoad(bool pauseBeforeFade)
    {
        // Stop video
        if (pauseBeforeFade && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }

        // Fade out into next scene
        if (fadeOutDuration > 0f)
        {
            yield return Fade(0f, 1f, fadeOutDuration);
        }
        else
        {
            fadeCanvasGroup.alpha = 1f;
        }

        videoPlayer.Stop();

        SceneManager.LoadScene(nextSceneName);
    }

    private System.Collections.IEnumerator Fade(float from, float to, float duration)
    {
        if (duration <= 0f)
        {
            fadeCanvasGroup.alpha = to;
            yield break;
        }

        float elapsed = 0f;
        fadeCanvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            fadeCanvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        fadeCanvasGroup.alpha = to;
    }

    private void Update()
    {
        if (!hasFinishedOrSkipped)
        {
            // B button pressed
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                hasFinishedOrSkipped = true;
                StartCoroutine(FadeOutAndLoad(pauseBeforeFade: true));
            }
        }
    }
}
