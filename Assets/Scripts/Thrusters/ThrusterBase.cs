﻿/*
 * abstract base class for different thruster implementations that allow for different types of movements 
 * (e.g. basic ones only forward acceleration, others full 4 directional movement)
 * 
 * the thruster is, for now, not a trigger so it could collider with other debris in space. might be interesting
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class ThrusterBase : MonoBehaviour
{
    #region VARIABLES
    
    [Tooltip("The strength of a single thrust into any given direction")]
    [SerializeField]
    protected float m_thrust_strength = 1.0f;
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

    #region THRUSTER FUNCTIONS

    /// <summary>
    /// what to do when being prompted to thrust forwards
    /// </summary>
    /// <param name="n_rb">the rigidbody that gets thrusted</param>
    public virtual void forward(Rigidbody n_rb)
    {}

    /// <summary>
    /// what to do when being prompted to thrust backwards
    /// </summary>
    /// <param name="n_rb">the rigidbody that gets thrusted</param>
    public virtual void backward(Rigidbody n_rb)
    {}

    /// <summary>
    /// what to do when being prompted to thrust to the right
    /// </summary>
    /// <param name="n_rb">the rigidbody that gets thrusted</param>
    public virtual void right(Rigidbody n_rb)
    {}

    /// <summary>
    /// what to do when being prompted to thrust to the left
    /// </summary>
    /// <param name="n_rb">the rigidbody that gets thrusted</param>
    public virtual void left(Rigidbody n_rb)
    {}

    /// <summary>
    /// placeholder, not sure what to do with this yet
    /// </summary>
    /// <param name="n_rb"></param>
    public virtual void boost(Rigidbody n_rb)
    {}

    /// <summary>
    /// function that gets called when the left mouse button has been clicked
    /// </summary>
    /// <param name="n_rb">the rigidbody of the object this thruster is thrusting</param>
    public virtual void leftclick(Rigidbody n_rb)
    {}

    /// <summary>
    /// function that gets called when the left mouse button has been clicked
    /// </summary>
    /// <param name="n_rb">the rigidbody of the object this thruster is thrusting</param>
    public virtual void rightclick(Rigidbody n_rb)
    {}

    /// <summary>
    /// function that gets called when the middle mouse button has been clicked
    /// </summary>
    /// <param name="n_rb">the rigidbody of the object this thruster is thrusting</param>
    public virtual void middleclick(Rigidbody n_rb)
    { }

    #endregion
}
