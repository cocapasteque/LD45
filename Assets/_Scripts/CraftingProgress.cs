using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Crafting/Progress", fileName = "Progress")]
public class CraftingProgress : ScriptableObject
{
    public List<CraftingLevel> Levels;
}

[Serializable]
public class CraftingLevel
{
    public List<SpawnChance> ResourceSpawnPercentages;
    public float SpawnCooldown;
    public float PointsNeededToLevel;
}

[Serializable]
public class SpawnChance
{
    public int Index;
    public float Percentage;
}
