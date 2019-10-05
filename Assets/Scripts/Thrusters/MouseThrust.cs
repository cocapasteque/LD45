/*
 * thruster that reacts only to mouse leftclick and thrusts into the opposite viewing direction
 */

using UnityEngine;

public class MouseThrust : ThrusterBase
{
    public override void leftclick(Rigidbody n_rb)
    {
        base.leftclick(n_rb);

        n_rb.AddForce(-n_rb.transform.forward * m_thrust_strength);
    }
}
