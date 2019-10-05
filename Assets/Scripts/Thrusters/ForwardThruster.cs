
/*
 * Implementation of a simple thruster that can only thrust forwards into the direction the palyer is facing
 */

using UnityEngine;

public class ForwardThruster : ThrusterBase
{
    public override void forward(Rigidbody n_rb)
    {
        base.forward(n_rb);

        n_rb.AddForce(n_rb.transform.forward * m_thrust_strength);
    }
}
