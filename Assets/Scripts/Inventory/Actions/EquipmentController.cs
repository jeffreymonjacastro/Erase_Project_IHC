using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraAnchor;      // CenterEyeAnchor

    private void Awake()
    {
        if (cameraAnchor == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: camera anchor");
        }
    }

    public void Equip(ItemData item, int index)
    {
        if (item == null || item.prefab == null)
        {
            Debug.LogError("Equip called with null item or prefab");
            return;
        }

        Vector3 spawnPos = cameraAnchor.position + cameraAnchor.forward * 1.0f;
        Quaternion spawnRot = Quaternion.LookRotation(cameraAnchor.forward, Vector3.up);

        Instantiate(item.prefab, spawnPos, spawnRot);
    }
}
