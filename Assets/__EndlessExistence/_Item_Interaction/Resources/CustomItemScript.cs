using System.IO;
using UnityEditor;
using UnityEngine;

namespace __EndlessExistence._Item_Interaction.Resources
{
    public static  class CustomItemScript
    {
        [MenuItem("GameObject/Endless Existence/Create Item Object", false, 0)]
        static void CreateCustomObject(MenuCommand menuCommand)
        {
            // Search for the prefab file within the project
            string[] prefabFiles = Directory.GetFiles(Application.dataPath, "ItemPrefab.prefab", SearchOption.AllDirectories);

            // Check if the prefab file exists
            if (prefabFiles.Length == 0)
            {
                Debug.LogError("Prefab 'ItemPrefab.prefab' not found in the project.");
                return;
            }

            // Get the first prefab file found
            string prefabPath = prefabFiles[0];

            // Convert the absolute path to a relative path
            prefabPath = "Assets" + prefabPath.Substring(Application.dataPath.Length);

            // Load the prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            // Instantiate the prefab
            GameObject customObject = GameObject.Instantiate(prefab);
            customObject.name = "NewItem";

            // Set the parent and alignment
            GameObjectUtility.SetParentAndAlign(customObject, menuCommand.context as GameObject);

            // Register the GameObject creation for Undo/Redo functionality
            Undo.RegisterCreatedObjectUndo(customObject, "Create " + customObject.name);

            // Make the newly created GameObject the active selection
            Selection.activeObject = customObject;
        }
    }
}
