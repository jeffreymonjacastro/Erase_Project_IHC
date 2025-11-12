using UnityEngine;

public class PuertaRotar : MonoBehaviour
{
    // Este script está en el HIJO (por ejemplo, Cube_10)
    public float anguloApertura = 90.0f;
    public float velocidadSuavizado = 2.0f;

    // CAMBIO 1: ¡Ahora es pública!
    // Esto creará un campo en el inspector para que arrastres el script.
    public PuertaTrigger trigger;

    private Quaternion _rotacionCerrada;
    private Quaternion _rotacionAbiertaAfuera;
    private Quaternion _rotacionAbiertaAdentro;
    private bool _abrir = false;

    void Start()
    {
        // CAMBIO 2: ¡Hemos borrado la búsqueda automática!
        // trigger = GetComponentInParent<PuertaTrigger>(); // <-- LÍNEA BORRADA

        // Dejamos una advertencia por si te olvidas de asignarlo en el Inspector
        if (trigger == null)
        {
            Debug.LogError("¡ERROR! No has asignado la variable 'Trigger' en el Inspector del objeto '" + gameObject.name + "'");
        }

        // El resto sigue igual
        _rotacionCerrada = transform.localRotation;
        _rotacionAbiertaAfuera = _rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        _rotacionAbiertaAdentro = _rotacionCerrada * Quaternion.Euler(0, -anguloApertura, 0);
    }

    void Update()
    {
        // CAMBIO 3: Si el trigger no está asignado, no hagas nada.
        if (trigger == null) return;

        // Esta es la línea 36 que daba error. Ahora está protegida.
        _abrir = trigger.debeAbrirse;

        // Elegir la rotación objetivo según desde qué lado se entra
        Quaternion rotacionObjetivo;

        if (_abrir)
        {
            rotacionObjetivo = trigger.abrirHaciaAfuera ? _rotacionAbiertaAfuera : _rotacionAbiertaAdentro;
        }
        else
        {
            rotacionObjetivo = _rotacionCerrada;
        }

        // Interpolamos suavemente
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            rotacionObjetivo,
            Time.deltaTime * velocidadSuavizado
        );
    }
}