using System.Collections;
using System.Collections.Generic;
using EndlessExistence.Inventory.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class EE_ItemDetailContainer : MonoBehaviour
{
    private Button button;
    public EE_ItemDetail itemDetail;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => EE_Inventory.Instance.SetDetailPanel(
            itemDetail.itemName , itemDetail.itemDescription, itemDetail.itemImage , itemDetail.itemCurrentQuantity , itemDetail.maxStack));
    }
}
