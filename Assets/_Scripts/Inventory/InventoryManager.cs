using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class InventoryManager : SerializedMonoBehaviour
{
    public static InventoryManager Instance;

    public List<GameItem> Items;
    public List<Blueprint> Bps;

    public List<CraftingSlot> Slots;

    public Dictionary<GameItem, int> AvailableItems;
    public Transform ResourcePanel;
    public GameObject ResourceButtonPrefab;
    public Transform InventoryPanel;
    public GameObject BlueprintPrefab;
    public Transform BlueprintPanel;

    private Dictionary<GameItem, DragResource> ResourceButtons;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        AvailableItems = new Dictionary<GameItem, int>();
        ResourceButtons = new Dictionary<GameItem, DragResource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddItem(Items[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem(Items[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem(Items[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddItem(Items[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RemoveItem(Items[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RemoveItem(Items[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            RemoveItem(Items[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            RemoveItem(Items[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddBlueprint(Bps[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AddBlueprint(Bps[1]);
        }
    }

    public void AddItem(GameItem item)
    {
        if (AvailableItems.ContainsKey(item))
        {
            AvailableItems[item]++;
            ResourceButtons[item].SetAmount(AvailableItems[item]);
        }
        else
        {
            AvailableItems.Add(item, 1);
            GameObject go = Instantiate(ResourceButtonPrefab, ResourcePanel);
            go.GetComponent<DragResource>().Init(item, 1);
            ResourceButtons.Add(item, go.GetComponent<DragResource>());
        }
    }

    public void RemoveItem(GameItem item)
    {
        AvailableItems[item]--;
        ResourceButtons[item].SetAmount(AvailableItems[item]);
    }

    public void AddToNextEmptySlot(GameItem item)
    {
        foreach(CraftingSlot slot in Slots)
        {
            if (!slot.Filled)
            {
                slot.AddToSlot(item);
                break;
            }
        }
    }

    public void AddBlueprint(Blueprint bp)
    {
        GameObject go = Instantiate(BlueprintPrefab, BlueprintPanel);
        go.GetComponent<InventoryBlueprint>().Init(bp.recipe);
    }

    public void CreateObject()
    {
        GameItem[] items = GetCurrentCraftingBar();
        CraftingSystem.Instance.Craft(items);
        ClearAllSlots(false);
    }

    public GameItem[] GetCurrentCraftingBar()
    {
        List<GameItem> bar = new List<GameItem>();
        foreach(CraftingSlot slot in Slots)
        {
            if (slot.Filled)
            {
                bar.Add(slot.CurrentItem);
            }
        }
        return bar.ToArray();
    }

    public void ClearAllSlots(bool refund)
    {
        foreach (CraftingSlot slot in Slots)
        {
            if (slot.Filled)
            {
                slot.ClearSlot(refund);
            }
        }
    }
}