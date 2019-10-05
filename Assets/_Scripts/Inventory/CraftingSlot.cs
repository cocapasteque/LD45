using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{  
    public Image Icon;

    [HideInInspector]
    public bool Filled;
    [HideInInspector]
    public GameItem CurrentItem;

    private void Start()
    {
        Filled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag);
        AddToSlot(eventData.pointerDrag.GetComponent<DragResource>().Item);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CurrentItem != null)
        {
            ClearSlot(true);
        }
    }
    public void AddToSlot(GameItem item)
    {
        CurrentItem = item;
        InventoryManager.Instance.RemoveItem(item);
        Icon.sprite = item.icon;
        Icon.gameObject.SetActive(true);
        Filled = true;
    }

    public void ClearSlot(bool refund)
    {      
        if (refund)
        {
            InventoryManager.Instance.AddItem(CurrentItem);
        }
        CurrentItem = null;
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        Filled = false;
    }
}
