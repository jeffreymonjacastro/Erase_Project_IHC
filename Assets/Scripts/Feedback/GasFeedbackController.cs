using UnityEngine;

public class GasFeedbackController : MonoBehaviour
{
    [Header("Overlays")]
    [Tooltip("Overlay for general gas danger (e.g. red tint)")]
    [SerializeField] private CanvasGroup gasDangerOverlay;

    [Tooltip("Overlay shown when mask is equipped (e.g. mask vignette)")]
    [SerializeField] private CanvasGroup maskOverlay;

    [Header("Danger Settings")]
    [Tooltip("How fast the danger overlay alpha reacts to changes")]
    [SerializeField] private float dangerLerpSpeed = 5f;
    [SerializeField] private float dangerWithMaskModifier = 0.3f; // E [0, 1]

    private bool _maskEquipped;
    private float _targetDangerAlpha;
    private float _currentDangerAlpha;

    private void Awake()
    {
        if (gasDangerOverlay == null)
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
        // Smoothly interpolate danger overlay alpha
        _currentDangerAlpha = Mathf.Lerp(_currentDangerAlpha, _targetDangerAlpha, Time.deltaTime * dangerLerpSpeed);

        gasDangerOverlay.alpha = _currentDangerAlpha;
    }

    public void SetMaskEquipped(bool equipped)
    {
        _maskEquipped = equipped;

        maskOverlay.alpha = equipped ? 1f : 0f;
    }

    /// <summary>
    /// dangerLevel in [0, 1].
    /// </summary>
    public void SetDangerLevel(float dangerLevel)
    {
        // Only dangerous if mask not equipped
        float effective = _maskEquipped ? dangerWithMaskModifier * dangerLevel : dangerLevel;
        _targetDangerAlpha = Mathf.Clamp01(effective);
    }
}
