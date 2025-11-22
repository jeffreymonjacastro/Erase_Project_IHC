using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveAssetPreview
{
    [MenuItem("Tools/Save Selected Asset Preview")]
    static void SavePreview()
    {
        Object obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogWarning("No asset selected.");
            return;
        }

        Texture2D preview = AssetPreview.GetAssetPreview(obj);
        if (preview == null)
        {
            Debug.LogWarning("Preview not available yet.");
            return;
        }

        string path = Application.dataPath + "/Art/UI/Icons/Preview_" + obj.name + ".png";
        File.WriteAllBytes(path, preview.EncodeToPNG());

        Debug.Log("Saved preview to: " + path);
    }
}
