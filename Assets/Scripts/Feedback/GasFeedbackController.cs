using UnityEngine;

public class GasFeedbackController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform playerHead;
    [SerializeField] private GasZone gasZone;

    [Header("Overlays")]
    [Tooltip("Overlay shown when mask is equipped (e.g. mask vignette)")]
    [SerializeField] private CanvasGroup maskOverlay;
    [Tooltip("Overlay for general gas danger (e.g. red tint)")]
    [SerializeField] private CanvasGroup dangerOverlay;

    [Header("Mask Settings")]
    [SerializeField] private float maskFadeSpeed = 6f;

    [Header("Danger Vignette Settings")]
    [SerializeField] private float vignetteMaxAlpha = 0.3f;
    [SerializeField] private float vignetteFadeSpeed = 7f;
    [SerializeField] private float vignettePulseSpeed = 0.7f;

    private float minIntensity;
    private float maxIntensity;
    private float intensityRange;

    private bool maskEquipped;
    private float currentVignetteAlpha;

    private void Awake()
    {
        if (gasZone == null)
        {
            Debug.LogError("[GasFeedbackController] Missing reference: gas zone");
        }

        if (dangerOverlay == null)
        {
            Debug.LogError("[GasFeedbackController] Missing reference: gas danger overlay");
        }

        if (maskOverlay == null)
        {
            Debug.LogError("[GasFeedbackController] Missing reference: mask overlay");
        }
    }

    private void Start()
    {
        minIntensity = CalculateIntensity(gasZone.InnerRadius);
        maxIntensity = CalculateIntensity(gasZone.DangerRadius);
        intensityRange = maxIntensity - minIntensity;
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
        float d = gasZone.GetDistanceToLeak(playerHead.position);

        if (d < gasZone.DangerRadius)
        {
            // Pulse between 0.6 and 1.0 based on time
            float pulse = 0.7f + 0.3f * Mathf.Sin(Time.time * vignettePulseSpeed);

            // If mask is on, reduce the effect
            float maskFactor = maskEquipped ? 0.4f : 1f;

            // Calculate the intensity
            float intensity = CalculateIntensity(d);
            float t = (intensityRange - intensity) / intensityRange;

            targetAlpha = t * vignetteMaxAlpha * pulse * maskFactor;
        }

        currentVignetteAlpha = Mathf.MoveTowards(
            currentVignetteAlpha,
            targetAlpha,
            vignetteFadeSpeed * Time.deltaTime
        );

        dangerOverlay.alpha = currentVignetteAlpha;
    }

    private float CalculateIntensity(float distance)
    {
        return distance * distance;
    }
}
