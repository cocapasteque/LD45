using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 1f * Time.deltaTime);
    }
}
