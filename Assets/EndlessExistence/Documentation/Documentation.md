
---

# Easy Interaction System - Usage Documentation

## Interaction Configuration

1. **Installation:**
   - Obtain the Easy Interaction System package from the Unity Asset Store.
   - Import the package into your Unity project.
   - Ensure that your player character has the tag "Player" and is assigned to the layer "Player".

2. **Creating Interactable Objects:**
   - Open the Unity Editor.
   - Navigate to **Hierarchy > Endless Existence > Create new Item** to create a new interactable object.
   - Alternatively, locate the desired item/object prefab in the asset browser and drag it into your scene.
   - You can find the prefabs at the following path: **Assets/EndlessExistence/Item Interaction/Prefabs/ObjectPrefab**.

3. **Configuring Interactable Objects:**
   - After creating the object, select it in the hierarchy.
   - In the Inspector window, add the appropriate type of item script based on the desired behavior.
   - For example, to create a health item, add the **EE_Health** script to the object.
   - To create a new type of object with custom behavior, inherit from the **ObjectContainer** script.

4. **Adding Item Details:**
   - To provide additional information about the object, create a new Scriptable Object.
   - From the asset browser, navigate to **Create > Inventory Item > ItemDetails**.
   - Fill in the necessary information such as name, description, icon, etc., in the created Scriptable Object.

## Inspect Configuration

1. **Setting Up Inspection Feature:**
   - To enable the inspect feature, locate the inspect camera prefab in the asset browser.
   - Drag the inspect camera prefab into your scene.
   - Make the inspect camera a child of your player character.
   - You can find the inspect camera prefab at: **Assets/EndlessExistence/Item Interaction/Prefabs/InspectCamera.prefab**.
   - Add the **EE_InspectObject** script to the object you want to inspect.

## Inventory Setup

1. **Setting Up Inventory:**
   - To implement the inventory system, locate the inventory prefab in the asset browser.
   - Drag the inventory prefab into your scene.
   - You can find the inventory prefab at: **Assets/EndlessExistence/Inventory/Prefabs/EE_Inventory.prefab**.
   - Customize the inventory settings and appearance as needed.

## Mobile Control Setup

1. **Dragging Mobile UI:**
   - Drag the MobileUI prefab from Assets/EndlessExistence/Common/MobileUI.prefab

## Additional Information from README

- **About Tags:**
   - Ensure that your player character is tagged as "Player" and assigned to the "Player" layer.

- **Editor Menu:**
   - From the Unity Editor menu, you can create new items/objects easily.

- **Item Scripting:**
   - To create custom behavior for objects, you can inherit from the **ObjectContainer** script.

- **Inspect Camera Setup:**
   - The inspect camera prefab should be made a child of the player character to enable inspection feature.

- **Inventory Scriptable Object:**
   - Use the provided Scriptable Object to add details and information about inventory items.

---