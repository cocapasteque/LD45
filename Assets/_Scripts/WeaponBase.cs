/*
 * abstract base class for weapons
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class WeaponBase : MonoBehaviour
{
    [Tooltip("cooldown between shots [in seconds]")]
    [SerializeField]
    protected float m_cooldown;
    /// <summary>
    /// the cooldown between shots for this weapon
    /// </summary>
    public float Cooldown
    {
        get { return m_cooldown; }
    }

    /// <summary>
    /// every weapon needs to have something happen when you shoot
    /// </summary>
    public abstract void shoot();
}
