using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScrap : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _rotationSpeed;
    private GameItem _item;

    private float _creationTime;

    public void Init(Vector3 velocity, Vector3 rotationSpeed, GameItem item)
    {
        _velocity = velocity;
        _rotationSpeed = rotationSpeed;
        _item = item;
        _creationTime = Time.time;
    }

    private void Update()
    {
        transform.Translate(_velocity * Time.deltaTime, Space.World);
        transform.Rotate(_rotationSpeed * Time.deltaTime);

        if (Time.time - _creationTime > 30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        InventoryManager.Instance.AddItem(_item);
        Destroy(gameObject);
    }
}
