using UnityEditor;
using UnityEngine;

namespace EndlessExistence.Editor_Resources
{
    public static class CreateSlidingDoubleDoor
    {
        [MenuItem("GameObject/Endless Existence/Doors/Create Sliding Double Door", false, 2)]
        static void CreateCustomObject(MenuCommand menuCommand)
        {
            string prefabPath =
                @"Assets\EndlessExistence\Item Interaction\Prefabs\ObjectPrefab\SlidingDoubleDoor.prefab";
            //Debug.Log(prefabPath);
            // Load the prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            // Instantiate the prefab
            GameObject customObject = GameObject.Instantiate(prefab);
            customObject.name = "SlidingDoubleDoor";

            // Set the parent and alignment
            GameObjectUtility.SetParentAndAlign(customObject, menuCommand.context as GameObject);

            // Register the GameObject creation for Undo/Redo functionality
            Undo.RegisterCreatedObjectUndo(customObject, "Create " + customObject.name);

            // Make the newly created GameObject the active selection
            Selection.activeObject = customObject;
        }
    }
}
