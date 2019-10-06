using UnityEngine;
using UnityEngine.Serialization;

public class LaserWeapon : WeaponBase
{
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
    
    
    public override void Shoot(float flySpeed)
    {
        base.Shoot(flySpeed);
        Debug.Log("Shooting with Laser");

        var mousePos = Utils.GetMousePositionOn2DPlane();

        if (!mousePos.HasValue) return;
        
        var beam = Instantiate(laserBeam);
        beam.transform.position = m_origin.position;
        beam.transform.LookAt(mousePos.Value);
        
        beam.GetComponent<Projectile>()?.Launch(projectileSpeed, damage);
    }
}