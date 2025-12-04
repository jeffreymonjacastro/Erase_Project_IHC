using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GasZone : MonoBehaviour
{
    [Header("Radii")]
    [Tooltip("Distance from leak center where maximum danger is felt")]
    [SerializeField] private float innerRadius = 5f;

    [Tooltip("Distance from leak center where gas effect starts")]
    [SerializeField] private float outerRadius = 10f;

    [Header("References")]
    [SerializeField] private Transform leakCenter;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private GasFeedbackController gasFeedbackController;

    [Tooltip("Non-trigger collider that physically blocks the player inside the inner radius")]
    [SerializeField] private SphereCollider innerBlockerCollider;  // isTrigger = false

    private SphereCollider triggerCollider; // outer trigger

    private void Reset()
    {
        // Outer trigger
        triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = outerRadius;

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
        triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = outerRadius;

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
        // Wall ON when no mask, OFF when protected
        if (innerBlockerCollider != null && equipmentController != null)
        {
            innerBlockerCollider.enabled = !equipmentController.HasGasProtection;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Vector3 playerPos = other.transform.position;
        float distance = Vector3.Distance(playerPos, leakCenter.position);

        // Outside outer radius: no effect
        if (distance > outerRadius)
        {
            gasFeedbackController.SetDangerLevel(0f);
            return;
        }

        // Map [innerRadius, outerRadius] to [0,1]
        float clamped = Mathf.Clamp(distance, innerRadius, outerRadius);
        float range = outerRadius - innerRadius;
        float t = 1f - ((clamped - innerRadius) / range);

        gasFeedbackController.SetDangerLevel(t);
        // NOTE: no pushback here; physical collider handles blocking.
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        gasFeedbackController.SetDangerLevel(0f);
    }
}
