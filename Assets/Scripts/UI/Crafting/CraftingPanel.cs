using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;

public class CraftingPanel : MonoBehaviour, IDropHandler {

    private CraftingObject objectOnPanel
    {
        get
        {
            return transform.Find("ObjectOnPanel").GetComponentInChildren<CraftingObject>(); 
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (InventoryItem.ms_itemBeingDragged == null)
            return;

        InventoryItem item = InventoryItem.ms_itemBeingDragged.GetComponent<InventoryItem>();
        if (item == null)
            return;

        if (!objectOnPanel)
        {
            DialogueManager.ShowAlert("Rien n'est sur le panneau pour crafting");
        }
        else
        {
            // Craft item
            objectOnPanel.m_relative2DItem.CraftItem(item);
        }
        
    }
}