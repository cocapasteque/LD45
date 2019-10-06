using UnityEngine;

public class LaserWeapon : WeaponBase
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Shooting with Laser");
    }
}