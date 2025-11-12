using UnityEngine;

// Asegúrate de que estás usando el namespace de Oculus, si no lo tienes,
// puedes obtener el "Oculus Integration" desde el Asset Store de Unity.
using OVR;

public class ControladorInventario : MonoBehaviour
{
    [Tooltip("Arrastra aquí el objeto 'Panel' que está dentro de tu 'Inventario'")]
    public GameObject panelInventario;

    [Tooltip("Elige qué botón de Oculus abrirá/cerrará el inventario. 'Two' es B o Y.")]
    public OVRInput.Button botonInventario = OVRInput.Button.Two; // Botón B (derecho) o Y (izquierdo)

    [Tooltip("Elige qué control puede activar el inventario.")]
    public OVRInput.Controller controlador = OVRInput.Controller.RTouch; // Control derecho

    void Start()
    {
        // Nos aseguramos de que el inventario comience cerrado (oculto)
        if (panelInventario != null)
        {
            panelInventario.SetActive(false);
        }
    }

    void Update()
    {
        // Comprobamos cada frame si se ha presionado el botón asignado en el control asignado
        if (OVRInput.GetDown(botonInventario, controlador))
        {
            ToggleInventario();
        }

        // Opcional: Si quieres que CUALQUIER control (izquierdo o derecho) pueda hacerlo:
        // if (OVRInput.GetDown(botonInventario, OVRInput.Controller.LTouch) || OVRInput.GetDown(botonInventario, OVRInput.Controller.RTouch))
        // {
        //     ToggleInventario();
        // }
    }

    /// <summary>
    /// Activa o desactiva el panel del inventario.
    /// </summary>
    public void ToggleInventario()
    {
        if (panelInventario != null)
        {
            // Invierte el estado actual del panel (si está activo lo desactiva, y viceversa)
            panelInventario.SetActive(!panelInventario.activeSelf);
        }
    }
}