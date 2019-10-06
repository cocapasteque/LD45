using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float _hitPoints;
    private float _aggroRange;
    private float _acceleration;
    private Rigidbody _rb;
    private PlayerScript _player;
    private WeaponBase _weapon;
    
    public void Init(float hitPoints, float aggroRange, float acceleration, Vector3 startVelocity, WeaponBase weapon)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = startVelocity;
        _player = FindObjectOfType<PlayerScript>();
        _hitPoints = hitPoints;
        _aggroRange = aggroRange;
        _acceleration = acceleration;
        _weapon = weapon;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        _rb.AddForce((_player.transform.position - transform.position).normalized * _acceleration);
        transform.LookAt(_player.transform.position);
        if (Vector3.Distance(_player.transform.position, transform.position) > _aggroRange)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamages(float hp)
    {
        _hitPoints -= hp;
        if (_hitPoints <= 0)
        {
            Explode();
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            _weapon.Shoot(_rb.velocity.magnitude);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
