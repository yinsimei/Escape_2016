using UnityEngine;
using System.Collections;

public class CraftingPanel : MonoBehaviour
{
    public Crafting crafting;
    public Inventory inventory;

    private bool isShowing = false;

    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;
        // Run show animation
        transform.GetComponent<Animator>().Play("InventoryPanel_Show");
    }

    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;
        // Run hide animation
        transform.GetComponent<Animator>().Play("InventoryPanel_Hide");

        // Reset Panel
    }
}
