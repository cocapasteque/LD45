/*
 * abstract base class for weapons
 */

using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Tooltip("cooldown between shots [in seconds]")]
    [SerializeField]
    protected float m_cooldown;
    public float Cooldown => m_cooldown;
    
    private bool m_on_cooldown;
    public bool On_Cooldown => m_on_cooldown;

    [SerializeField] protected Transform m_origin;
    
    /// <summary>
    /// every weapon needs to have something happen when you shoot. when overriding, always call this base function since it will track cooldown
    /// </summary>
    public virtual void Shoot(float flySpeed)
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
