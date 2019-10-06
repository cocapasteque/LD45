using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingSystem : SerializedMonoBehaviour
{
    public static CraftingSystem Instance = null;

    public List<Recipe> recipes;
    public List<Blueprint> blueprints;
    public Dictionary<Blueprint, bool> unlockedBlueprints;
    public CraftingProgress Progress;

    [HideInInspector]
    public int CraftingScore;
    [HideInInspector]
    public int CurrentLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Creating instance");
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("instance already exists.");
            Destroy(this);
        }

        CraftingScore = 0;
        CurrentLevel = 0;
        unlockedBlueprints = new Dictionary<Blueprint, bool>();
        foreach (Blueprint bp in blueprints)
        {
            unlockedBlueprints.Add(bp, false);
        }
    }

    public Recipe GetRecipeForItem(GameItem item)
    {
        return recipes.FirstOrDefault(x => x.result == item);
    }

    public GameItem Craft(params GameItem[] ingredients)
    {
        var recipe = new Dictionary<GameItem, int>();
        foreach (var item in ingredients)
        {
            if (recipe.ContainsKey(item))
            {
                recipe[item]++;
            }
            else
            {
                recipe.Add(item, 1);
            }
        }

        var result = recipes.FirstOrDefault(x => x.CheckRecipe(recipe))?.result;
        if (result)
        {
            Debug.Log($"Crafted {result.itemName}");
            HandleBlueprintUnlock(GetRecipeForItem(result));
            CraftingScore += ingredients.Sum(x => x.CraftingValue);
            CheckLevel();
        }

        return result;
    }

    private void CheckLevel()
    {
        if (CraftingScore > Progress.Levels[CurrentLevel].PointsNeededToLevel)
        {
            CurrentLevel++;
        }
    }

    private void HandleBlueprintUnlock(Recipe recipe)
    {
        var bp = unlockedBlueprints.FirstOrDefault(x => x.Key.recipe == recipe).Key;
        if (bp)
        {
            Debug.Log($"Found recipe {recipe.itemName} for bp {bp.itemName}");
            UnlockBlueprint(bp);
        }
    }

    public void UnlockBlueprint(Blueprint bp)
    {
        if (unlockedBlueprints[bp]) return;
        
        unlockedBlueprints[bp] = true;
        InventoryManager.Instance.AddBlueprint(bp);
    }
}