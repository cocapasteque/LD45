using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScrap : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _rotationSpeed;
    [HideInInspector]
    public GameItem _item;

    public void Init(Vector3 velocity, Vector3 rotationSpeed, GameItem item)
    {
        _velocity = velocity;
        _rotationSpeed = rotationSpeed;
        _item = item;
    }

    private void Update()
    {
        transform.Translate(_velocity * Time.deltaTime, Space.World);
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InventoryManager.Instance.AddItem(_item);
            Destroy(gameObject);
        }
    }
}
