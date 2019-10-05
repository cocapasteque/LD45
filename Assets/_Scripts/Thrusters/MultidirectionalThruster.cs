/*
 * thruster that can thrust into any given direction, by which i mean up, down, left, right
 */
using UnityEngine;

public class MultidirectionalThruster : ThrusterBase
{
    public override void forward(PlayerScript n_player)
    {
        base.forward(n_player);
        n_player.RigidBody.AddForce(n_player.Player_Mesh.transform.forward * m_thrust_strength);
    }

    public override void backward(PlayerScript n_player)
    {
        base.backward(n_player);
        n_player.RigidBody.AddForce(-n_player.Player_Mesh.transform.forward * m_thrust_strength);
    }

    public override void left(PlayerScript n_player)
    {
        base.left(n_player);
        n_player.RigidBody.AddForce(-n_player.Player_Mesh.transform.right * m_thrust_strength);
    }

    public override void right(PlayerScript n_player)
    {
        base.right(n_player);
        n_player.RigidBody.AddForce(n_player.Player_Mesh.transform.right * m_thrust_strength);
    }
}
