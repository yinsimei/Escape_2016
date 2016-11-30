using UnityEngine;
using UnityEngine.Assertions;

public class CraftingObject : MonoBehaviour
{
    private Crafting m_crafting;

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
    }

    private void SetOutOfCrafting()
    {
        Assert.AreEqual(transform.parent, m_crafting.ObjectOnPanel);
        SetObjectParent(m_crafting.HiddenObject);
    }

    private void SetObjectParent(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }
}
