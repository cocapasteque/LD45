/*
 * class for thruster. Add to object to make it pickup-able and then added to player's thrusters
 * 
 * the thruster is, for now, not a trigger so it could collider with other debris in space. might be interesting
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Thruster : MonoBehaviour
{
    #region VARIABLES
    
    [Tooltip("The aggregated force over one second that will be thrusted forward by this thruster.")]
    [SerializeField]
    protected float m_thrust_strength = 10.0f;
    /// <summary>
    /// The strength of a single thrust into any given direction
    /// </summary>
    public float Thrust_Strength
    {
        get { return m_thrust_strength; }
    }

    #endregion
    
    #region UNITY LIFECYCLE

    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.tag = "Thruster";

        // for the time being, we do not want this to be a trigger so it could collider and push around other debris in space
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    #endregion
}
