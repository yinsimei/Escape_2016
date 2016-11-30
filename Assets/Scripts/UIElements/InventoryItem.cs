using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;

public class InventoryItem : MonoBehaviour
{
    public CraftingObject related3DObject;
    private Inventory m_inventory;
    private int m_nNumInInventory;
    private bool m_bInInventory
    {
        get { return (m_nNumInInventory >= 0); }
    }
    
    // Initialisation
    void Start()
    {
        m_nNumInInventory = -1;
        m_inventory = transform.parent.parent.GetComponent<Inventory>();
        Assert.IsNotNull(m_inventory);
        Assert.IsNotNull(related3DObject);
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
            DialogueManager.ShowAlert("Clef trouvé");
            SetItemIntoInventory();
        }
        else
        {
            DialogueManager.ShowAlert("Clef utilisé");
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
            Assert.AreEqual(m_inventory.inventoryCells.GetChild(i).childCount, 0);
            m_inventory.inventoryCells.GetChild(i + 1).transform.
                GetComponent<InventoryItem>().SetItemParent(
                m_inventory.inventoryCells.GetChild(i).transform
                );
        }

        --m_inventory.m_nItemNb;
        m_nNumInInventory = -1;
    }

    public void SetItemParent(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }
}
