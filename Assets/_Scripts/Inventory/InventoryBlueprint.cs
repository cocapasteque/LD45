using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryBlueprint : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public GameObject ComponentPrefab;
    public Transform ComponentsParent;

    public void Init(Recipe recipe)
    {
        Name.text = recipe.itemName;
        foreach(KeyValuePair<GameItem, int> kvp in recipe.compos)
        {
            GameObject go = Instantiate(ComponentPrefab, ComponentsParent);
            go.transform.localScale = Vector3.one;
            go.GetComponent<InventoryBlueprintComponent>().Init(kvp.Key.icon, kvp.Value);
        }
    }
}
