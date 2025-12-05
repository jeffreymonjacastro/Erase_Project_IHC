using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameSequenceController : MonoBehaviour
{
    public static EndGameSequenceController Instance { get; private set; }

    [Header("Outro")]
    [Tooltip("Name of the outro scene as in Build Settings")]
    [SerializeField] private string outroSceneName = "OutroScene";

    [Tooltip("Extra delay after the audio finishes before switching scene")]
    [SerializeField] private float delayAfterAudio = 0.25f;

    [Header("Audio")]
    [Tooltip("AudioSource used to play the finale sound")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Sound to play when the gas leak source is picked up")]
    [SerializeField] private AudioClip pickupClip;

    private bool _hasTriggered;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("[EndGameSequenceController] Duplicate instance, destroying this one.");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogWarning("[EndGameSequenceController] No AudioSource assigned or found on this GameObject.");
        }
    }

    public void TriggerFinale()
    {
        if (_hasTriggered)
        {
            return;
        }

        _hasTriggered = true;
        Debug.Log("[EndGameSequenceController] Finale triggered.");
        StartCoroutine(PlayAndTransitionRoutine());
    }

    private IEnumerator PlayAndTransitionRoutine()
    {
        float waitTime = 0f;

        if (audioSource != null && pickupClip != null)
        {
            audioSource.clip = pickupClip;
            audioSource.Play();
            waitTime = pickupClip.length;
        }
        else
        {
            Debug.LogWarning("[EndGameSequenceController] Missing AudioSource or pickupClip, skipping audio.");
        }

        // Wait for audio + extra delay
        yield return new WaitForSeconds(waitTime + delayAfterAudio);

        if (!string.IsNullOrEmpty(outroSceneName))
        {
            Debug.Log($"[EndGameSequenceController] Loading outro scene '{outroSceneName}'.");
            SceneManager.LoadScene(outroSceneName);
        }
        else
        {
            Debug.LogError("[EndGameSequenceController] Outro scene name is empty. Set it in the inspector.");
        }
    }
}