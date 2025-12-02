using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GasZone : MonoBehaviour
{
    [Header("Radii")]
    [Tooltip("Distance from leak center where maximum danger is felt")]
    [SerializeField] private float innerRadius = 2f;

    [Tooltip("Distance from leak center where gas effect starts")]
    [SerializeField] private float outerRadius = 5f;

    [Header("Pushback")]
    [Tooltip("How strongly the player is pushed out of the inner radius when unprotected")]
    [SerializeField] private float pushBackStrength = 3f;

    [Tooltip("Root object of the player (used for pushback)")]
    [SerializeField] private Transform playerRoot;

    [Header("References")]
    [SerializeField] private Transform leakCenter;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private GasFeedbackController gasFeedbackController;

    private SphereCollider _collider;

    private void Reset()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        outerRadius = _collider.radius;
        innerRadius = outerRadius * 0.4f;
    }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;

        if (leakCenter == null)
            leakCenter = transform;

        if (playerRoot == null)
        {
            Debug.LogWarning("[GasZone] Missing reference: player root");
        }

        if (equipmentController == null)
        {
            Debug.LogError("[GasZone] Missing reference: equipment controller");
        }

        if (gasFeedbackController == null)
        {
            Debug.LogError("[GasZone] Missing reference: gas feedback controller");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Identify the player using tag
        if (!other.CompareTag("Player"))
            return;

        if (playerRoot == null)
        {
            // Try to find player root from collider hierarchy once
            playerRoot = other.GetComponentInParent<Transform>();
        }

        Vector3 playerPos = other.transform.position;
        float distance = Vector3.Distance(playerPos, leakCenter.position);

        // Outside outer radius: no effect
        if (distance > outerRadius)
        {
            gasFeedbackController.SetDangerLevel(0f);
            return;
        }

        float t = 0f;
        float clamped = Mathf.Clamp(distance, innerRadius, outerRadius); // restrict to [innerRadius, outerRadius]
        float range = outerRadius - innerRadius;
        t = 1f - ((clamped - innerRadius) / range); // Map distance -> [0, 1]

        gasFeedbackController.SetDangerLevel(t);

        if (equipmentController.HasGasProtection)
            return;

        // Inside inner radius + no protection => pushback
        if (distance <= innerRadius && playerRoot != null)
        {
            Vector3 dir = (playerRoot.position - leakCenter.position).normalized;
            Vector3 targetPos = leakCenter.position + dir * innerRadius;
            playerRoot.position = Vector3.Lerp(playerRoot.position, targetPos, Time.deltaTime * pushBackStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Leaving the zone => clear danger overlay
        gasFeedbackController.SetDangerLevel(0f);
    }
}
