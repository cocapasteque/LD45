/*
 * thruster that reacts only to mouse leftclick and thrusts into the opposite viewing direction
 */

using UnityEngine;

public class MouseThrust : ThrusterBase
{
    public override void rightclick(PlayerScript n_player)
    {
        base.leftclick(n_player);
        
        n_player.RigidBody.AddForce(n_player.Player_Mesh.transform.forward * m_thrust_strength);
    }
}
