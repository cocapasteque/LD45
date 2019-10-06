using UnityEngine;
using UnityEngine.Serialization;

public class LaserWeapon : WeaponBase
{
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private float projectileSpeed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }
    
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Shooting with Laser");

        var mousePos = Utils.GetMousePositionOn2DPlane();

        if (!mousePos.HasValue) return;
        
        var beam = Instantiate(laserBeam);
        beam.transform.position = m_origin.position;
        beam.transform.LookAt(mousePos.Value);
        
        beam.GetComponent<Projectile>()?.Launch(projectileSpeed);
    }
}