using UnityEngine;

public class PuertaTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool debeAbrirse = false;

    [HideInInspector]
    public bool abrirHaciaAfuera = true;

    private Transform camaraJugador;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Sigue buscando al jugador
        if (other.CompareTag("Player"))
        {
            // 2. CAMBIO IMPORTANTE:
            // Ya no usamos Camera.main. Buscamos la cámara DENTRO del OVRCameraRig.
            // La ruta es OVRCameraRig -> TrackingSpace -> CenterEyeAnchor
            camaraJugador = other.transform.Find("TrackingSpace/CenterEyeAnchor");

            // 3. Si por alguna razón no la encuentra así, este es un plan B:
            if (camaraJugador == null)
            {
                // Buscará la primera cámara hija que encuentre dentro del Player
                camaraJugador = other.GetComponentInChildren<Camera>()?.transform;
            }

            // 4. Si AÚN no la encuentra, te avisará en la consola.
            if (camaraJugador == null)
            {
                Debug.LogError("PuertaTrigger: No se pudo encontrar la cámara (CenterEyeAnchor) dentro del 'Player'.");
                return; // No hacer nada más
            }

            // El resto de la lógica sigue igual
            debeAbrirse = true;
            Vector3 direccionJugador = camaraJugador.position - transform.position;
            float dot = Vector3.Dot(transform.forward, direccionJugador);
            abrirHaciaAfuera = dot < 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            debeAbrirse = false;
            camaraJugador = null;
        }
    }
}