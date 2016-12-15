using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class CraftingText : MonoBehaviour
{
    public Text textField;
    public GameObject craftingInfoPanel;

    public void SetText(string p_text)
    {
        textField.text = p_text;
    }

    public void SetText(InventoryItem p_item)
    {
        Assert.IsNotNull(p_item, "item input for crafting text setting is null");

        if (p_item.isUsable)
        {
            textField.text = p_item.itemInfo;
            craftingInfoPanel.GetComponent<CanvasGroup>().alpha = 0f;
        }
        else
        {
            craftingInfoPanel.GetComponent<CanvasGroup>().alpha = 1f;
            textField.text = p_item.m_craftingInfo.craftingInfoText;
            craftingInfoPanel.GetComponentInChildren<CraftingCells>().SetCells(p_item.m_craftingInfo);
        }
    }

    public void ResetText()
    {
        textField.text = "";
        craftingInfoPanel.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void UpdateCraftingText(InventoryItem p_item)
    {
        SetText(p_item);
    }
}
