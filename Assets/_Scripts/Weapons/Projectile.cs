using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float m_travelSpeed;
    private bool m_launched = false;
    private float m_damage = 0;
    [SerializeField] private float destroyDelay;    
    [SerializeField] private bool player;
    
    private void Update()
    {
        if (m_launched)
        {
            transform.Translate(Time.deltaTime * m_travelSpeed * Vector3.forward);
        }
    }

    public void Launch(float travelSpeed, float damage)
    {
        m_travelSpeed = travelSpeed;
        m_launched = true;
        m_damage = damage;
        Destroy(this.gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {               
                other.GetComponent<EnemyBehavior>().TakeDamages(m_damage);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerScript>().TakeDamages(m_damage);
                Destroy(this.gameObject);               
            }
        }
    }
}