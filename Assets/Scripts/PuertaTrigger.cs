using UnityEngine;

public class PuertaTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool debeAbrirse = false;

    [HideInInspector]
    public bool abrirHaciaAfuera = true; // Determina la dirección de apertura

    private Transform jugador;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.transform;
            debeAbrirse = true;

            // Vector desde la puerta hacia el jugador
            Vector3 direccionJugador = jugador.position - transform.position;

            // Producto punto para saber de qué lado está el jugador
            float dot = Vector3.Dot(transform.forward, direccionJugador);

            // Si el jugador está frente a la puerta (dot > 0), se abre hacia afuera
            // Si está detrás (dot < 0), se abre hacia adentro
            abrirHaciaAfuera = dot < 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            debeAbrirse = false;
            jugador = null;
        }
    }
}
