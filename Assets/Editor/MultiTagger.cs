#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class SelectorDeTags
{
    // Define el nombre del Tag aquí
    private const string TARGET_TAG = "Grabbable"; // <--- ¡CAMBIA ESTO!

    [MenuItem("Tools/Forzar Selección/Seleccionar por Tag")]
    private static void SelectObjectsByTag()
    {
        // 1. Encuentra todos los GameObjects activos en la escena con el Tag especificado
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(TARGET_TAG);

        if (taggedObjects.Length > 0)
        {
            // 2. Convierte el array de GameObjects en un array de Unity.Object para la selección
            Object[] selectionObjects = new Object[taggedObjects.Length];
            for (int i = 0; i < taggedObjects.Length; i++)
            {
                selectionObjects[i] = taggedObjects[i];
            }

            // 3. Establece la selección en la Jerarquía
            Selection.objects = selectionObjects;

            Debug.Log($"Éxito: Se han seleccionado {taggedObjects.Length} objetos con el Tag '{TARGET_TAG}'.");
        }
        else
        {
            Debug.LogWarning($"Advertencia: No se encontraron objetos con el Tag '{TARGET_TAG}' en la escena activa.");
            Selection.objects = new Object[0]; // Limpia la selección
        }
    }
}
#endif