using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryBlueprintComponent : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Count;

    public void Init(Sprite sprite, int count)
    {
        Icon.sprite = sprite;
        Count.text = count.ToString();
    }
}
