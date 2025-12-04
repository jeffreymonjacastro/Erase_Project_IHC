using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GasZone : MonoBehaviour
{
    [Header("Radii")]
    [Tooltip("Distance from leak center where maximum danger is felt")]
    [SerializeField] private float innerRadius = 10;

    [Tooltip("Distance from leak center where gas effect starts")]
    [SerializeField] private float outerRadius = 25f;

    [Header("Pushback")]
    [Tooltip("How strongly the player is pushed out of the inner radius when unprotected")]
    [SerializeField] private float pushBackStrength = 0.5f;

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
        _collider.radius = outerRadius;
    }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;

        if (leakCenter == null)
            leakCenter = transform;

        if (playerRoot == null)
        {
            Debug.LogError("[GasZone] Missing reference: player root");
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

        Vector3 playerPos = other.transform.position;
        float distance = Vector3.Distance(playerPos, leakCenter.position);

        // Outside outer radius: no effect
        if (distance > outerRadius)
        {
            gasFeedbackController.SetDangerLevel(0f);
            return;
        }


        Debug.Log($"!! distance = {distance}");
        float t = 0f;
        float clamped = Mathf.Clamp(distance, innerRadius, outerRadius); // restrict to [innerRadius, outerRadius]
        Debug.Log($"!! clamped distance = {clamped}");
        float range = outerRadius - innerRadius;
        t = 1f - ((clamped - innerRadius) / range);

        Debug.Log($"!! final = {t}");

        gasFeedbackController.SetDangerLevel(t);

        if (equipmentController.HasGasProtection)
            return;

        // Inside inner radius + no protection => pushback
        /*if (distance <= innerRadius)
        {
            Vector3 dir = (playerRoot.position - leakCenter.position).normalized;
            Vector3 targetPos = leakCenter.position + dir * innerRadius;
            playerRoot.position = Vector3.Lerp(playerRoot.position, targetPos, Time.deltaTime * pushBackStrength);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Leaving the zone => clear danger overlay
        gasFeedbackController.SetDangerLevel(0f);
    }
}
