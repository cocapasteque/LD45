using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragResource : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler, IEndDragHandler
{   
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Counter;      
    public GameObject Icon;
    public GameItem Item;

    private int _amount;
    private GameObject _currentlyDraggingObj;

    public void Init(GameItem item, int amount = 1)
    {
        Name.text = item.name;
        Icon.GetComponent<Image>().sprite = item.icon;
        SetAmount(amount);
        Item = item;
    }

    public void SetAmount(int newAmount)
    {
        _amount = newAmount;
        Counter.text = "x" + newAmount;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (_amount > 0)
        {
            _currentlyDraggingObj = Instantiate(Icon, InventoryManager.Instance.InventoryPanel);
            _currentlyDraggingObj.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_currentlyDraggingObj);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_currentlyDraggingObj != null)
        {
            _currentlyDraggingObj.transform.position = eventData.pointerCurrentRaycast.screenPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.AddToNextEmptySlot(Item);
    }   
}
