using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensorFeedbackController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SensorMeasurementController measurement;

    [Header("UI")]
    [SerializeField] private GameObject sensorPanelRoot;
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private TextMeshProUGUI ppmLabel;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image dangerBar; // optional fill bar

    [Header("Colors")]
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color cautionColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;

    [Header("Audio")]
    [SerializeField] private AudioSource beepAudio;
    [Tooltip("Slowest beep interval (seconds).")]
    [SerializeField] private float beepMinInterval = 0.8f;
    [Tooltip("Fastest beep interval (seconds).")]
    [SerializeField] private float beepMaxInterval = 0.15f;
    [Tooltip("Base pitch for low concentration.")]
    [SerializeField] private float basePitch = 0.8f;
    [Tooltip("Additional pitch at maximum concentration.")]
    [SerializeField] private float pitchRange = 0.5f;

    public bool IsOn { get; private set; }

    private float beepTimer;

    private void Awake()
    {
        if (measurement == null)
        {
            Debug.LogError("[SensorFeedbackController] Missing reference: sensor measurement controller");
        }

        if (sensorPanelRoot == null)
        {
            Debug.LogError("[SensorFeedbackController] Missing reference: sensor panel root");
        }

        if (titleLabel == null)
        {
            Debug.LogError("[SensorFeedbackController] Missing reference: title label");
        }

        if (ppmLabel == null)
        {
            Debug.LogError("[SensorFeedbackController] Missing reference: PPM label");
        }

        if (backgroundImage == null)
        {
            Debug.LogError("[SensorFeedbackController] Missing reference: background image");
        }

        if (dangerBar == null)
        {
            Debug.LogWarning("[SensorFeedbackController] Missing reference: danger bar");
        }

        if (beepAudio == null)
        {
            Debug.LogWarning("[SensorFeedbackController] Missing reference: beep audio");
        }

        sensorPanelRoot.SetActive(false);
    }

    private void Update()
    {
        if (!IsOn)
            return;

        UpdateHud();
        UpdateBeep();
    }

    public void TurnOn()
    {
        IsOn = true;
        beepTimer = 0f;
        sensorPanelRoot.SetActive(true);
    }

    public void TurnOff()
    {
        IsOn = false;
        sensorPanelRoot.SetActive(false);

        if (beepAudio != null && beepAudio.isPlaying)
            beepAudio.Stop();
    }

    private void UpdateHud()
    {
        float ppm = measurement.CurrentPpm;
        float norm = Mathf.Clamp01(measurement.CurrentNormalized);

        ppmLabel.text = $"PPM: {ppm:0}";

        titleLabel.text = "Toxic Gas Sensor";

        if (dangerBar != null)
            dangerBar.fillAmount = norm;

        Color zoneColor = safeColor;
        switch (measurement.CurrentZoneState)
        {
            case GasZoneState.Safe:
                zoneColor = safeColor;
                break;
            case GasZoneState.Caution:
                zoneColor = cautionColor;
                break;
            case GasZoneState.Danger:
                zoneColor = dangerColor;
                break;
        }

        backgroundImage.color = zoneColor;
    }

    private void UpdateBeep()
    {
        if (beepAudio == null)
            return;

        // Use normalized concentration for beep frequency
        float norm = Mathf.Clamp01(measurement.CurrentNormalized);

        // Interpolate beep interval: higher conc => faster beeps
        float interval = Mathf.Lerp(beepMinInterval, beepMaxInterval, norm);

        beepTimer += Time.deltaTime;
        if (beepTimer >= interval)
        {
            beepTimer = 0f;

            beepAudio.pitch = basePitch + norm * pitchRange;
            beepAudio.Play();
        }
    }
}
