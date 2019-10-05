﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance = null;

    public List<Recipe> recipes;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
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

        return recipes.FirstOrDefault(x => x.CheckRecipe(recipe))?.result;
    }
}