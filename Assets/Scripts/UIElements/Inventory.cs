using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerClickHandler
{

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
        m_nItemNb = 0;
        m_eMode = EInventoryMode.e_Hidden;
        m_unLockAction = null;
        Assert.IsNotNull(inventoryCells);
        Assert.IsNotNull(invisibleRoot);
    }

    public void Show()
    {
        m_eMode = EInventoryMode.e_Crafting;
        GetComponent<Animator>().Play("Show");
    }

    public void ShowItems(UnlockAction p_unLockAction)
    {
        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnInventoryShow", gameObject, SendMessageOptions.DontRequireReceiver);

        m_eMode = EInventoryMode.e_Solo;
        GetComponent<Animator>().Play("Show");
        m_unLockAction = p_unLockAction;
    }

    public void Hide()
    {
        inventoryCells.GetComponent<ToggleGroup>().SetAllTogglesOff();
        GetComponent<Animator>().Play("Hide");
        m_eMode = EInventoryMode.e_Hidden;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_eMode != EInventoryMode.e_Solo)
            return;

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnInventoryHide", gameObject, SendMessageOptions.DontRequireReceiver);
        Hide();
    }
}
