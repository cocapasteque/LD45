using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : WeaponBase
{
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float maxScatteringAngle;

    public override void Shoot(float flySpeed)
    {
        GameObject player = FindObjectOfType<PlayerScript>().gameObject;
        base.Shoot(flySpeed);

        var beam = Instantiate(laserBeam);
        beam.transform.position = m_origin.position;
        beam.transform.LookAt(player.transform);
        beam.transform.rotation = Quaternion.Euler(beam.transform.rotation.x, beam.transform.rotation.y + Random.Range(-maxScatteringAngle, maxScatteringAngle), beam.transform.rotation.z);

        beam.GetComponent<Projectile>()?.Launch(projectileSpeed + flySpeed);
    }
}
