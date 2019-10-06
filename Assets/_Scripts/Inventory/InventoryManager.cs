using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using JetBrains.Annotations;

public class InventoryManager : SerializedMonoBehaviour
{
    public static InventoryManager Instance;

    public List<GameItem> Items;
    public List<Blueprint> Bps;
    public List<GameItem> Equipment;
    
    public List<CraftingSlot> Slots;

    public Dictionary<GameItem, int> AvailableItems;
    public Transform ResourcePanel;
    public GameObject ResourceButtonPrefab;
    public Transform InventoryPanel;
    public GameObject BlueprintPrefab;
    public Transform BlueprintPanel;

    private Dictionary<GameItem, DragResource> ResourceButtons;
    private PlayerScript _player;
    
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
        Equipment = new List<GameItem>();

        _player = FindObjectOfType<PlayerScript>();
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
            AddItem(Items[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddItem(Items[5]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AddItem(Items[6]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AddItem(Items[7]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddItem(Items[8]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AddItem(Items[9]);
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
        var created = CraftingSystem.Instance.Craft(items);
        if (created != null && created.type == ItemType.Equipment)
        {
            Equipment.Add(created);
            if (created.prefab.GetComponent<Thruster>() != null)
            {
                _player.AddThruster(created.prefab.GetComponent<Thruster>());
            }
        }

        if (created != null && created.type == ItemType.Ingredient)
        {
            if (AvailableItems.ContainsKey(created)) AvailableItems[created]++;
            else AvailableItems.Add(created, 1);
        }

        if (created != null && created.type == ItemType.Repair)
        {
            FindObjectOfType<PlayerScript>().Heal(created.CraftingValue);
        }

        if (created != null && created.type == ItemType.Finder)
        {
            //TODO: FIND EARTH
        }
        
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