using UnityEngine;
using UnityEditor;

public class FixMaterialSlots : EditorWindow
{
    // Cambié el nombre del menú para que sea más claro
    [MenuItem("Herramientas/Arreglar Materiales Rotos (Multi-Slot)")]
    public static void FixSelectedObjectsMaterials()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("Error", "No seleccionaste ningún objeto. Por favor, selecciona los 'walls' en la Jerarquía.", "OK");
            return;
        }

        int fixedCount = 0;

        // Registrar todos los cambios para poder hacer Undo
        Undo.RecordObjects(selectedObjects, "Fix Multi-Slot Pink Materials");

        foreach (GameObject obj in selectedObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

            // Omitir si no hay renderer o no tiene materiales
            if (renderer == null || renderer.sharedMaterials.Length == 0)
            {
                continue;
            }

            Material[] currentMaterials = renderer.sharedMaterials;

            // --- INICIO DE LA LÓGICA MEJORADA ---

            // 1. Revisar si el objeto tiene el problema: el primer slot es NULO
            if (currentMaterials[0] == null)
            {
                Material validMaterial = null;

                // 2. Buscar el material válido, empezando DESDE EL FINAL
                for (int i = currentMaterials.Length - 1; i >= 0; i--)
                {
                    if (currentMaterials[i] != null)
                    {
                        validMaterial = currentMaterials[i];
                        break; // ¡Encontrado!
                    }
                }

                // 3. Si encontramos un material válido, aplicamos el arreglo
                if (validMaterial != null)
                {
                    // Crear un nuevo array de materiales que SÓLO contenga el material bueno
                    Material[] newMaterials = new Material[] { validMaterial };

                    // Asignar el nuevo array al objeto
                    renderer.sharedMaterials = newMaterials;

                    fixedCount++;
                    EditorUtility.SetDirty(obj); // Marcar el objeto como modificado
                }
            }
            // --- FIN DE LA LÓGICA MEJORADA ---
        }

        // Mostrar un mensaje de éxito
        EditorUtility.DisplayDialog("Completado", "Se procesaron " + selectedObjects.Length + " objetos.\nSe arreglaron " + fixedCount + " objetos.", "OK");
    }
}