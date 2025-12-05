using UnityEngine;

public class SensorMeasurementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GasZone gasZone;
    [Tooltip("Typically the player head / main camera.")]
    [SerializeField] private Transform headAnchor;

    [Header("Sampling")]
    [Tooltip("Seconds between each measurement update.")]
    [SerializeField] private float updateInterval = 0.1f;

    private float timeSinceLastUpdate;

    public float CurrentPpm { get; private set; }
    public float CurrentNormalized { get; private set; }
    public float DistanceToSource { get; private set; }
    public GasZoneState CurrentZoneState { get; private set; }

    private void Awake()
    {
        if (gasZone == null)
        {
            Debug.LogError("[SensorMeasurementController] Missing reference: gas zone");
        }

        if (headAnchor == null)
        {
            Debug.LogError("[SensorMeasurementController] Missing reference: head anchor");
        }
    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate < updateInterval)
            return;

        UpdateMeasurement();
        timeSinceLastUpdate = 0f;
    }

    private void UpdateMeasurement()
    {
        Vector3 pos = headAnchor.position;

        DistanceToSource = gasZone.GetDistanceToLeak(pos);
        CurrentNormalized = gasZone.GetNormalizedConcentration(pos);
        CurrentPpm = gasZone.GetPpmAtPosition(pos);

        float inner = gasZone.InnerRadius;
        float outer = gasZone.OuterRadius;

        if (DistanceToSource > outer)
            CurrentZoneState = GasZoneState.Safe;
        else if (DistanceToSource > inner)
            CurrentZoneState = GasZoneState.Caution;
        else
            CurrentZoneState = GasZoneState.Danger;
    }
}
