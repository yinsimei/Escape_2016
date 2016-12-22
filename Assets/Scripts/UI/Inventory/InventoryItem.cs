using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CraftingObject related3DObject;
    public bool disappearAfterUsed = true;
    public string itemName = "object";
    public string itemInfo = "Information d'objet";

    public bool isUsable
    {
        get { return (m_craftingInfo == null || m_craftingInfo.isCraftingCompleted); }
    }

    public bool needCrafting
    {
        get { return (m_craftingInfo != null && !m_craftingInfo.isCraftingCompleted); }
    }

    private Inventory m_inventory;
    private int m_nNumInInventory;
    private bool m_bInInventory
    {
        get { return (m_nNumInInventory >= 0); }
    }
    [HideInInspector]
    public ItemCraftingInfo m_craftingInfo;

    [HideInInspector]
    public static GameObject ms_itemBeingDragged;

    // Initialisation
    void Start()
    {
        m_nNumInInventory = -1;
        m_inventory = transform.parent.parent.GetComponent<Inventory>();
        Assert.IsNotNull(m_inventory);
        Assert.IsNotNull(related3DObject);
        related3DObject.m_relative2DItem = this;
        m_craftingInfo = GetComponent<ItemCraftingInfo>();
    }

    public void SetInInventory(bool inInventory)
    {
        if (m_bInInventory == inInventory)
        {
            if (inInventory)
                Debug.LogError("Object " + name + " already in inventory");
            else
                Debug.LogError("Object" + name + "doesn't exist in inventory");
            return;
        }

        if (inInventory)
        {
            DialogueManager.ShowAlert(itemName + " trouvé");
            SetItemIntoInventory();
        }
        else
        {
            DialogueManager.ShowAlert(itemName + " utilisé");
            SetItemOutOfInventory();
        }

        Assert.AreEqual(m_bInInventory, inInventory);
    }

    private void SetItemIntoInventory()
    {
        Assert.AreEqual(transform.parent, m_inventory.invisibleRoot);
        m_nNumInInventory = m_inventory.m_nItemNb;
        Transform cellParent = m_inventory.inventoryCells.GetChild(m_nNumInInventory);
        Assert.AreEqual(cellParent.childCount, 0);

        SetItemParent(cellParent);
        ++m_inventory.m_nItemNb;
    }

    private void SetItemOutOfInventory()
    {
        SetItemParent(m_inventory.invisibleRoot);
        for (int i = m_nNumInInventory; i < m_inventory.m_nItemNb - 1; ++i)
        {
            
            Transform cellToFill = m_inventory.inventoryCells.GetChild(i);
            Transform nextCell = m_inventory.inventoryCells.GetChild(i + 1);
            Assert.AreEqual(cellToFill.childCount, 0);
            Assert.AreEqual(nextCell.childCount, 1);

            nextCell.GetComponentInChildren<InventoryItem>().SetItemParent(cellToFill);
        }

        --m_inventory.m_nItemNb;
        m_nNumInInventory = -1;
    }

    public void SetItemParent(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (related3DObject.isOnPanel)
            return;

        ms_itemBeingDragged = this.gameObject;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ms_itemBeingDragged == null)
            return;

        Canvas myCanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();

        // if the render mode of canvas is ScreenSpaceOverlay
        if (myCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            // we can directly set the mouse position to it
            ms_itemBeingDragged.transform.position = Input.mousePosition;
        }
        else
        {
            // we need to transform mouse position to fit it
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            ms_itemBeingDragged.transform.position = myCanvas.transform.TransformPoint(pos);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ms_itemBeingDragged == null)
            return;

        // Reset values for next drag
        ms_itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Reset position
        transform.localPosition = Vector3.zero;
    }

    public void CraftItem(InventoryItem p_item)
    {
        if (m_craftingInfo == null || !m_craftingInfo.CraftItem(p_item))
        {
            DialogueManager.ShowAlert("On ne peut pas assembler ces deux objects");
        }
        else
        {
            DialogueManager.ShowAlert(itemName + " utilisé");
            p_item.SetItemOutOfInventory();
            related3DObject.m_crafting.m_craftingText.UpdateCraftingText(this);
        }
    }
}