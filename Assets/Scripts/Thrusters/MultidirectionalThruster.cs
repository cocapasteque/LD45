/*
 * thruster that can thrust into any given direction, by which i mean up, down, left, right
 */
using UnityEngine;

public class MultidirectionalThruster : ThrusterBase
{
    public override void forward(Rigidbody n_rb)
    {
        base.forward(n_rb);
        n_rb.AddForce(n_rb.transform.forward * m_thrust_strength);
    }

    public override void backward(Rigidbody n_rb)
    {
        base.backward(n_rb);
        n_rb.AddForce(-n_rb.transform.forward * m_thrust_strength);
    }

    public override void left(Rigidbody n_rb)
    {
        base.left(n_rb);
        n_rb.AddForce(-n_rb.transform.right * m_thrust_strength);
    }

    public override void right(Rigidbody n_rb)
    {
        base.right(n_rb);
        n_rb.AddForce(n_rb.transform.right * m_thrust_strength);
    }
}
