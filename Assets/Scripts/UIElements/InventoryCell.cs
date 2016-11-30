using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public void SetItemToCrafting()
    {
        bool isOn = transform.GetComponent<Toggle>().isOn;
        if (transform.childCount > 0)
        {
            Assert.AreEqual(transform.childCount, 1);
            transform.GetComponentInChildren<InventoryItem>().related3DObject.SetToCrafting(isOn);
        }
    }
}
