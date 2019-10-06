﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBehavior : MonoBehaviour
{
    private float _hitPoints;
    private float _aggroRange;
    private float _acceleration;
    private Rigidbody _rb;
    private PlayerScript _player;
    private WeaponBase[] _weapons;
    
    public void Init(float hitPoints, float aggroRange, float acceleration, Vector3 startVelocity)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = startVelocity;
        _player = FindObjectOfType<PlayerScript>();
        _hitPoints = hitPoints;
        _aggroRange = aggroRange;
        _acceleration = acceleration;
        _weapons = GetComponentsInChildren<WeaponBase>();
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
            foreach (var weapon in _weapons.Where(weapon => !weapon.On_Cooldown))
            {
                weapon.Shoot(_rb.velocity.magnitude);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Explode(float dmg = 0)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Explode(_hitPoints);
        }
    }
}
