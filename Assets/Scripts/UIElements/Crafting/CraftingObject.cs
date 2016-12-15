using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CraftingObject : MonoBehaviour
{
    public bool isOnPanel
    {
        get { return transform.parent.Equals(m_crafting.ObjectOnPanel); }
    }

    [HideInInspector]
    public InventoryItem m_relative2DItem;
    [HideInInspector]
    public Crafting m_crafting;
    private bool onCrafting;

    void Start()
    {
        onCrafting = false;
        m_crafting = transform.parent.parent.parent.GetComponent<Crafting>();
        Assert.IsNotNull(m_crafting);
    }

    public void SetToCrafting(bool toCrafting)
    {
        if (onCrafting == toCrafting)
        {
            if (!onCrafting)
                Debug.LogError("Object " + name + " is not on crafting panel");
            return;
        }

        onCrafting = toCrafting;
        if (toCrafting)
        {
            SetOnToCrafting();
        }
        else
        {
            SetOutOfCrafting();
        }
    }

    private void SetOnToCrafting()
    {
        Assert.AreEqual(transform.parent, m_crafting.HiddenObject);
        SetObjectParent(m_crafting.ObjectOnPanel);
        m_crafting.m_craftingText.SetText(m_relative2DItem);
    }

    private void SetOutOfCrafting()
    {
        Assert.AreEqual(transform.parent, m_crafting.ObjectOnPanel);
        SetObjectParent(m_crafting.HiddenObject);
        m_crafting.m_craftingText.ResetText();
    }

    private void SetObjectParent(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
    }
}
