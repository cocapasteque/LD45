using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlueprint : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _rotationSpeed;
    private Blueprint _item;

    private float _creationTime;

    public void Init(Vector3 velocity, Vector3 rotationSpeed, Blueprint item)
    {
        _velocity = velocity;
        _rotationSpeed = rotationSpeed;
        _item = item;
        _creationTime = Time.time;
    }

    private void Update()
    {
        transform.Translate(_velocity * Time.deltaTime, Space.World);
        Debug.Log(_velocity);
        transform.Rotate(_rotationSpeed * Time.deltaTime);

        if (Time.time - _creationTime > 30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        CraftingSystem.Instance.UnlockBlueprint(_item);
        Destroy(gameObject);
    }
}
