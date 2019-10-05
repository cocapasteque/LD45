using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTest : MonoBehaviour
{
    // Simulates the crafting bar.
    public GameItem[] craftingBar;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Craft();
    }

    void Craft()
    {
        var result = CraftingSystem.Instance.Craft(craftingBar);
        if (result)
        {
            Debug.Log($"Congrats ! You just crafted {result}");
        }
        else
        {
            Debug.Log($"Invalid recipe. You crafted nothing.");
        }
    }
}