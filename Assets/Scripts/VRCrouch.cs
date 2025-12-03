using UnityEngine;

public class CrouchSystem : MonoBehaviour
{
    [Header("Asignaciones")]
    [Tooltip("Arrastra aquí el objeto 'TrackingSpace' que está dentro de OVRCameraRig")]
    public Transform trackingSpace;

    [Header("Configuración")]
    [Tooltip("La tecla X en los mandos Quest corresponde a Button.Three")]
    public OVRInput.Button vrButton = OVRInput.Button.Three;

    [Tooltip("Tecla para probar en el Editor de Unity sin gafas")]
    public KeyCode editorKey = KeyCode.C;

    [Tooltip("Cuánto baja la cámara en metros (negativo)")]
    public float crouchDepth = -0.6f;

    private bool isCrouched = false;
    private float originalY;

    void Start()
    {
        if (trackingSpace != null)
        {
            originalY = trackingSpace.localPosition.y;
        }
        else
        {
            Debug.LogError("¡ERROR: Falta asignar el TrackingSpace en el script!");
        }
    }

    void Update()
    {
        // Funciona si pulsas el botón VR -O- si pulsas la tecla en el PC
        if (OVRInput.GetDown(vrButton) || Input.GetKeyDown(editorKey))
        {
            isCrouched = !isCrouched;
            ToggleCrouch();
        }
    }

    void ToggleCrouch()
    {
        if (trackingSpace == null) return;

        Vector3 pos = trackingSpace.localPosition;

        if (isCrouched)
        {
            // Bajar
            pos.y = originalY + crouchDepth;
        }
        else
        {
            // Subir (Restaurar)
            pos.y = originalY;
        }

        trackingSpace.localPosition = pos;
    }
}