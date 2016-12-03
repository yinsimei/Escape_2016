using UnityEngine;
using UnityEngine.Assertions;

public class CraftingPanel : MonoBehaviour
{
    private Crafting m_crafting;
    private Inventory m_inventory;

    private bool isShowing = false;

    void Start()
    {
        m_crafting = transform.GetComponentInChildren<Crafting>();
        m_inventory = transform.GetComponentInChildren<Inventory>();
        Assert.IsNotNull(m_crafting);
        Assert.IsNotNull(m_inventory);
    }

    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;
        // Run show animation
        m_crafting.Show();
        m_inventory.Show();
    }

    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;
        // Run hide animation
        m_crafting.Hide();
        m_inventory.Hide();

        // Reset Panel
    }
}
