﻿/*
 * abstract base class for weapons
 */

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class WeaponBase : MonoBehaviour
{
    [Tooltip("cooldown between shots [in seconds]")]
    [SerializeField]
    protected float m_cooldown;
    public float Cooldown => m_cooldown;

    private bool m_on_cooldown;
    public bool On_Cooldown => m_on_cooldown;

    /// <summary>
    /// every weapon needs to have something happen when you shoot. when overrideing, always call this base function since it will track cooldown
    /// </summary>
    public virtual void Shoot()
    {
        StartCoroutine(RunCooldown());
    }

    private IEnumerator RunCooldown()
    {
        m_on_cooldown = true;
        yield return new WaitForSeconds(m_cooldown);
        m_on_cooldown = false;
    }
}
