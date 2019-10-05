using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "New recipe")]
public class Recipe: SerializedScriptableObject
{
    public string itemName;
    public Dictionary<GameItem, int> compos;
    public GameItem result;

    public bool CheckRecipe(Dictionary<GameItem, int> items)
    {
        foreach (var item in items)
        {
            if (compos.ContainsKey(item.Key))
            {
                if (compos[item.Key] != item.Value) return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object other)
    {
        var recipe = other as Recipe;
        if (recipe == null) return false;
        return recipe.itemName == itemName;
    }
}