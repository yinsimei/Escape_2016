using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public static Inventory instance;

    public enum EInventoryMode
    {
        e_Hidden,   // When inventory is hidden
        e_Solo,     // When inventory is shown for using objects
        e_Crafting  // When inventory is shown in the crafting system
    }

    public Transform inventoryCells;
    public Transform invisibleRoot;

    [HideInInspector]
    public int m_nItemNb;
    [HideInInspector]
    public EInventoryMode m_eMode;
    [HideInInspector]
    public UnlockAction m_unLockAction;

    // Use this for initialization
    void Start ()
    {
        Assert.IsNull(instance);
        instance = this;

        m_nItemNb = 0;
        m_eMode = EInventoryMode.e_Hidden;
        m_unLockAction = null;
        Assert.IsNotNull(inventoryCells);
        Assert.IsNotNull(invisibleRoot);
    }

    // ShowCells and HideCells are functions only called by Crafting panel
    public void ShowCells()
    {
        m_eMode = EInventoryMode.e_Crafting;
        inventoryCells.GetComponent<InventoryCells>().Show();
    }

    public void HideCells()
    {
        inventoryCells.GetComponent<ToggleGroup>().SetAllTogglesOff();
        inventoryCells.GetComponent<InventoryCells>().Hide();
        m_eMode = EInventoryMode.e_Hidden;
    }

    // ShowInventory and HideInventory are functions called when there is a object to use
    public void ShowInventory(UnlockAction p_unLockAction)
    {
        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnInventoryShow", gameObject, SendMessageOptions.DontRequireReceiver);

        m_eMode = EInventoryMode.e_Solo;
        GetComponent<Animator>().SetTrigger("Show");
        inventoryCells.GetComponent<InventoryCells>().GetComponent<Animator>().SetTrigger("Show");
        m_unLockAction = p_unLockAction;
    }

    public void HideInventory()
    {
        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnInventoryHide", gameObject, SendMessageOptions.DontRequireReceiver);

        // Set all toggles off : to get object out of panel 
        inventoryCells.GetComponent<ToggleGroup>().SetAllTogglesOff();

        // Off
        GetComponent<Animator>().SetTrigger("Hide");
        inventoryCells.GetComponent<InventoryCells>().GetComponent<Animator>().SetTrigger("Hide");
        m_eMode = EInventoryMode.e_Hidden;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_eMode != EInventoryMode.e_Solo)
            return;

        HideInventory();
    }
}
