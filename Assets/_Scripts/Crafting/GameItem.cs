using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Item", fileName = "New item")]
public class GameItem : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public GameObject prefab;
    
    public Recipe Recipe => CraftingSystem.Instance.GetRecipeForItem(this);
    
    public override bool Equals(object other)
    {
        var item = other as GameItem;
        if (item == null) return false;

        return itemName == item.itemName;
    }

    public override string ToString()
    {
        return itemName;
    }
}

public enum ItemType
{
    Ingredient, Equipment
}