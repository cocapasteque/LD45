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
        
        Debug.Log(result ? $"Congrats ! You just crafted {result}" : $"Invalid recipe. You crafted nothing.");
    }
}