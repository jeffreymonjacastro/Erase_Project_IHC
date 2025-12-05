using UnityEngine;

public class GasZone : MonoBehaviour
{
    [Header("Radii")]
    [Tooltip("Distance from leak center where maximum danger is felt")]
    [SerializeField] private float innerRadius = 5f;


    [Tooltip("Distance from leak center where danger starts")]
    [SerializeField] private float dangerRadius = 14f;

    [Tooltip("Distance from leak center where gas effect starts")]
    [SerializeField] private float outerRadius = 22f;

    [Header("PPM Settings")]
    [Tooltip("Ambient CO2-like level when far away.")]
    [SerializeField] private float ambientPpm = 400f;

    [Tooltip("Max PPM near the leak.")]
    [SerializeField] private float maxPpm = 5000f;

    [Header("References")]
    [SerializeField] private Transform leakCenter;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private GasFeedbackController gasFeedbackController;

    [Tooltip("Non-trigger collider that physically blocks the player inside the inner radius")]
    [SerializeField] private SphereCollider innerBlockerCollider;  // isTrigger = false

    public float InnerRadius => innerRadius;
    public float DangerRadius => dangerRadius;
    public float OuterRadius => outerRadius;

    private void Reset()
    {
        // Try to auto-create inner blocker as a child if not set
        if (innerBlockerCollider == null)
        {
            GameObject innerObj = new GameObject("InnerBlocker");
            innerObj.transform.SetParent(transform, false);
            innerObj.transform.localPosition = Vector3.zero;

            innerBlockerCollider = innerObj.AddComponent<SphereCollider>();
            innerBlockerCollider.isTrigger = false;
        }

        innerBlockerCollider.radius = innerRadius;
    }

    private void Awake()
    {
        if (leakCenter == null)
            leakCenter = transform;

        if (innerBlockerCollider != null)
            innerBlockerCollider.radius = innerRadius;

        if (equipmentController == null)
            Debug.LogError("[GasZone] Missing reference: equipment controller");

        if (gasFeedbackController == null)
            Debug.LogError("[GasZone] Missing reference: gas feedback controller");
    }

    private void Update()
    {
        Debug.Log($"!! Has gas protection = {equipmentController.HasGasProtection}");
        // Wall ON when no mask, OFF when protected
        if (innerBlockerCollider != null)
        {
            innerBlockerCollider.enabled = !equipmentController.HasGasProtection;
        }
    }

    /// <summary>Distance from the provided world position to the leak center.</summary>
    public float GetDistanceToLeak(Vector3 worldPos)
    {
        return Vector3.Distance(worldPos, leakCenter.position);
    }

    /// <summary>Returns [0,1] normalized concentration based on distance.</summary>
    public float GetNormalizedConcentration(Vector3 worldPos)
    {
        float d = GetDistanceToLeak(worldPos);

        if (d >= outerRadius) return 0f;
        if (d <= innerRadius) return 1f;

        // 0 at outerRadius, 1 at innerRadius
        float t = Mathf.InverseLerp(outerRadius, innerRadius, d);
        return t;
    }

    /// <summary>Returns a "realistic-ish" PPM value for the given world position.</summary>
    public float GetPpmAtPosition(Vector3 worldPos)
    {
        float conc = GetNormalizedConcentration(worldPos);
        float ppm = ambientPpm + conc * (maxPpm - ambientPpm);
        return ppm;
    }
}
