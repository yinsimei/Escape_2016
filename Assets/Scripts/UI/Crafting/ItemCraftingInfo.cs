using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class ItemCraftingInfo : MonoBehaviour
{

    public InventoryItem[] itemsNeededForCrafting;
    public string craftingInfoText;
    public UnityEvent OnCraftingComplete;

    [HideInInspector]
    public bool[] m_pbItemCrafted;
    private int m_nCraftedItemNb;

    public bool isCraftingCompleted
    {
        get { return (m_nCraftedItemNb == itemsNeededForCrafting.Length); }
    }

    void Awake()
    {
        if (OnCraftingComplete == null)
            OnCraftingComplete = new UnityEvent();
    }

    void Start()
    {
        Assert.IsTrue(itemsNeededForCrafting.Length > 0, "No item added to crafting info!");
        m_pbItemCrafted = new bool[itemsNeededForCrafting.Length];
        for (int i = 0; i < m_pbItemCrafted.Length; ++i)
        {
            m_pbItemCrafted[i] = false;
        }
    }

    public bool CraftItem(InventoryItem p_item)
    {
        for (int i = 0; i < itemsNeededForCrafting.Length; ++i)
        {
            if (!m_pbItemCrafted[i] && itemsNeededForCrafting[i].Equals(p_item))
            {
                m_pbItemCrafted[i] = true;
                ++m_nCraftedItemNb;
                if (isCraftingCompleted)
                {
                    OnCraftingComplete.Invoke();
                }
                return true;
            }
        }
        return false;
    }
}