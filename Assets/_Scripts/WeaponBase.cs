/*
 * abstract base class for weapons
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// every weapon needs to have something happen when you shoot
    /// </summary>
    public abstract void shoot();
}
