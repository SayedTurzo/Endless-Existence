using UnityEditor;
using UnityEngine;

namespace EndlessExistence.Editor_Resources
{
    public static class CreateHealthItem
    {
        [MenuItem("GameObject/Endless Existence/Items/Create Health Item", false, 1)]
        static void CreateCustomObject(MenuCommand menuCommand)
        {
            string prefabPath = @"Assets\EndlessExistence\Item Interaction\Prefabs\ObjectPrefab\HealthItem.prefab";
            //Debug.Log(prefabPath);
            // Load the prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            // Instantiate the prefab
            GameObject customObject = GameObject.Instantiate(prefab);
            customObject.name = "HealthItem";

            // Set the parent and alignment
            GameObjectUtility.SetParentAndAlign(customObject, menuCommand.context as GameObject);

            // Register the GameObject creation for Undo/Redo functionality
            Undo.RegisterCreatedObjectUndo(customObject, "Create " + customObject.name);

            // Make the newly created GameObject the active selection
            Selection.activeObject = customObject;
        }
    }
}
