using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float m_travelSpeed;
    private bool m_launched = false;
    [SerializeField] private float destroyDelay;
    
    private void Update()
    {
        if (m_launched)
        {
            transform.Translate(Time.deltaTime * m_travelSpeed * Vector3.forward);
        }
    }

    public void Launch(float travelSpeed)
    {
        m_travelSpeed = travelSpeed;
        m_launched = true;
        Destroy(this.gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            
            //TODO Enemy collision handling.
        }
    }
}