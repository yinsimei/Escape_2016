using UnityEngine;
using UnityEngine.Assertions;

public class CraftingSystem : MonoBehaviour
{
    private Crafting m_crafting;
    private Inventory m_inventory;

    private bool isShowing = false;

    void Start()
    {
        m_crafting = transform.GetComponentInChildren<Crafting>();
        Assert.IsNotNull(m_crafting);

        m_inventory = transform.GetComponentInChildren<Inventory>();
        Assert.IsNotNull(m_inventory);
    }

    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;
        // Run show animation
        m_crafting.Show();
        m_inventory.ShowCells();

        // If there exist objects needed to be repaired, show an information text
        string craftingText = "";
        foreach (InventoryItem item in m_inventory.inventoryCells.GetComponentsInChildren<InventoryItem>())
        {
           if (item.needCrafting)
            {
                craftingText = craftingText + "Cliquez " + item.itemName + " pour le réparer\n";
            }
        }
        if (!string.IsNullOrEmpty(craftingText))
        {
            m_crafting.m_craftingText.SetText(craftingText);
        }
    }

    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;
        // Run hide animation
        m_crafting.Hide();
        m_inventory.HideCells();
    }
}
