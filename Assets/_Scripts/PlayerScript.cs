/* 
 * Script that manages pretty much everything that has to do with the player, including:
 * - movement
 * - combat
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerScript : MonoBehaviour
{
    #region VARIABLES

    // player has a dedicated reference to a thruster, since it so vital for the gameplay and things need to move quick
    private List<Thruster> m_thrusters;

    // as with thruster, the players weapon is a singularly important piece of equipment, so we have a direct reference for it
    private List<WeaponBase> m_weapons;

    // plane though the origin with world up vector, which represens the plane the game takes place on in top-down fashion
    private Plane m_game_plane;

    // some flags so I can track when I need to be watching cooldowns for shooting and when I should be accelerating
    private bool m_shooting = false;
    private bool m_thrusting = false;

    // coroutine we hold just in case we want to stop it again at some point
    private Coroutine m_shooting_coroutine;

    [Tooltip("the gameobject that represents the player in the scene, a.k.a. the naked guy")]
    [SerializeField]
    private GameObject m_player_mesh;

    // the camera that has the player in focus
    [Tooltip("the camera that focuses on this player. is on same hierarchy level as the character model")]
    [SerializeField]
    private Camera m_camera;

    // distance of the camera to the player mesh (directly above)
    private float m_default_camera_distance;
    
    private Rigidbody m_rb;
    /// <summary>
    /// the rigidbody of the player obejct. is on the base GO, which is parent to camera and playermesh GO
    /// </summary>
    public Rigidbody RigidBody
    {
        get { return m_rb; }
    }

    /// <summary>
    /// the gameobject that represents the player in the scene
    /// </summary>
    public GameObject Player_Mesh
    {
        get { return m_player_mesh; }
    }

    #endregion
    
    #region UNITY LIFECYCLE

    // Start is called before the first frame update
    void Awake()
    {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        m_rb.useGravity = false; // make sure to have gravity off, we are in space

        if (m_camera == null)
        {
            m_camera = Camera.main;
        }

        // make the default game plane through origin with world up
        m_game_plane = new Plane(Vector3.up, Vector3.zero);

        m_weapons = new List<WeaponBase>();
        m_thrusters = new List<Thruster>();
    }

    private void Start()
    {
        // give the player an initial push toward the thruster
        m_rb.AddForce(this.transform.forward * 20.0f);

        m_shooting_coroutine = StartCoroutine(shoot());

        m_default_camera_distance = (m_camera.transform.position - m_player_mesh.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // rotation logic
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 0.0f;
        if (m_game_plane.Raycast(ray, out distance))
        {
            Vector3 hit = ray.GetPoint(distance);
            m_player_mesh.transform.LookAt(hit);
        }

        // left = shooting
        if (Input.GetMouseButtonDown(0))
        {
            m_shooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_shooting = false;
        }

        // right = thrusting
        if (Input.GetMouseButton(1))
        {
            foreach (Thruster thruster in m_thrusters)
            {
                m_rb.AddForce(m_player_mesh.transform.forward * thruster.Thrust_Strength * Time.deltaTime);
            }
        }

        // the faster the player is, the further away the camera is from him
        m_camera.transform.position = m_player_mesh.transform.position + Vector3.up*(m_default_camera_distance + m_rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thruster")
        {
            // we need to set our thruster to the one we now collided with, and get rid of it visually be reparenting it and scaling to 0
            m_thrusters.Add(collision.gameObject.GetComponent<Thruster>());
            collision.transform.SetParent(this.transform);
            collision.transform.localScale = Vector3.zero;
        }
    }

    #endregion

    #region PLAYER FUNCTIONS

    /// <summary>
    /// coroutine that runs in the background the whiole time and will shoot when we have the left mouse button pressed
    /// </summary>
    /// <returns></returns>
    private IEnumerator shoot()
    {
        // dear lord have mercy, we are indenting all the way to hell now...get ready
        while (true)
        {
            if (m_shooting)
            {
                foreach (WeaponBase weapon in m_weapons)
                {
                    if (!weapon.On_Cooldown)
                    {
                        weapon.shoot();
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}
