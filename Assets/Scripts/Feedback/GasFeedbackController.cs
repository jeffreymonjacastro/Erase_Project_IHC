using UnityEngine;

public class GasFeedbackController : MonoBehaviour
{
    [Header("Overlays")]
    [Tooltip("Overlay shown when mask is equipped (e.g. mask vignette)")]
    [SerializeField] private CanvasGroup maskOverlay;
    [Tooltip("Overlay for general gas danger (e.g. red tint)")]
    [SerializeField] private CanvasGroup dangerOverlay;

    [Header("Mask Settings")]
    [SerializeField] private float maskFadeSpeed = 6f;

    [Header("Danger Vignette Settings")]
    [SerializeField] private float vignetteMaxAlpha = 0.15f;
    [SerializeField] private float vignetteFadeSpeed = 7f;
    [SerializeField] private float vignettePulseSpeed = 0.3f;

    private bool maskEquipped;
    private float gasIntensity; // 0..1, set by gas zones
    private float currentVignetteAlpha;

    private void Awake()
    {
        if (dangerOverlay == null)
        {
            Debug.LogError("[GasFeedbackController] Missing reference: gas danger overlay");
        }

        if (maskOverlay == null)
        {
            Debug.LogError("[GasFeedbackController] Missing reference: mask overlay");
        }
    }

    private void Update()
    {
        UpdateMaskOverlay();
        UpdateDangerVignette();
    }

    /// <summary>Call this from your equipment / inventory logic.</summary>
    public void SetMaskEquipped(bool equipped)
    {
        maskEquipped = equipped;
    }

    /// <summary>
    /// Call this from gas zones. intensity is 0..1
    /// 0 = no gas, 1 = max danger.
    /// </summary>
    public void SetDangerLevel(float intensity)
    {
        gasIntensity = intensity;
    }

    // --- Internals ---

    private void UpdateMaskOverlay()
    {
        float target = maskEquipped ? 1f : 0f;
        maskOverlay.alpha = Mathf.MoveTowards(
            maskOverlay.alpha,
            target,
            maskFadeSpeed * Time.deltaTime
        );
    }

    private void UpdateDangerVignette()
    {
        float targetAlpha = 0f;

        if (gasIntensity > 0f)
        {
            // Pulse between 0.6 and 1.0 based on time
            float pulse = 0.7f + 0.3f * Mathf.Sin(Time.time * vignettePulseSpeed);

            // If mask is on, reduce the effect:
            float maskFactor = maskEquipped ? 0.25f : 1f;

            targetAlpha = gasIntensity * vignetteMaxAlpha * pulse * maskFactor;
        }

        currentVignetteAlpha = Mathf.MoveTowards(
            currentVignetteAlpha,
            targetAlpha,
            vignetteFadeSpeed * Time.deltaTime
        );

        dangerOverlay.alpha = currentVignetteAlpha;
    }
}
