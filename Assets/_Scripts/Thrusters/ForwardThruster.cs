
/*
 * Implementation of a simple thruster that can only thrust forwards into the direction the palyer is facing
 */

using UnityEngine;

public class ForwardThruster : ThrusterBase
{
    public override void forward(PlayerScript n_player)
    {
        base.forward(n_player);

        n_player.RigidBody.AddForce(n_player.Player_Mesh.transform.forward * m_thrust_strength);
    }
}
