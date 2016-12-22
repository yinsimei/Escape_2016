using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CraftingCells : MonoBehaviour
{    
    public void SetCells(ItemCraftingInfo p_info)
    {
        Assert.IsTrue(transform.childCount >= p_info.itemsNeededForCrafting.Length, "Not enough cells for items needed for crafting");

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (i < p_info.itemsNeededForCrafting.Length)
            {
                // Set sprite
                child.GetChild(0).GetComponent<Image>().sprite = p_info.itemsNeededForCrafting[i].transform.GetComponent<Image>().sprite;
                // Set alpha according to whether the item has been crafted
                child.GetComponent<CanvasGroup>().alpha = p_info.m_pbItemCrafted[i] ? 1f : 0.5f;
            }
            else
            {
                // Hide others
                child.GetComponent<CanvasGroup>().alpha = 0f;
            }

        }
    }
}
