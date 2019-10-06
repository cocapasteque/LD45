using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Blueprint", fileName = "New Blueprint")]
public class Blueprint : SerializedScriptableObject
{
    public string itemName;
    public Recipe recipe;
    public int level;

    public override bool Equals(object other)
    {
        var bp = other as Blueprint;
        if (bp == null) return false;
        return bp.itemName == itemName;
    }
}