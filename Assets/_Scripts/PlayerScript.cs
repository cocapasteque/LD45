/* 
 * Script that manages pretty much everything that has to do with the player, including:
 * - movement
 * - combat
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerScript : MonoBehaviour
{
    #region VARIABLES

    // player has a dedicated reference to a thruster, since it so vital for the gameplay and things need to move quick
    private ThrusterBase m_thruster;

    // as with thruster, the players weapon is a singularly important piece of equipment, so we have a direct reference for it
    private WeaponBase m_weapon;

    // plane though the origin with world up vector, which represens the plane the game takes place on in top-down fashion
    private Plane m_game_plane;

    [Tooltip("the gameobject that represents the player in the scene, a.k.a. the naked guy")]
    [SerializeField]
    private GameObject m_player_mesh;

    // the camera that has the player in focus
    [Tooltip("the camera that focuses on this player. is on same hierarchy level as the character model")]
    [SerializeField]
    private Camera m_camera;

    [Tooltip("The mouse sensitivity with which the player rotates")]
    [SerializeField]
    private float m_rotation_speed = 10.0f;
    
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
    }

    private void Start()
    {
        // give the player an initial push toward the thruster
        m_rb.AddForce(this.transform.forward * 20.0f);
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

        process_input();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thruster")
        {
            if (m_thruster != null)
            {
                Destroy(m_thruster.gameObject);
                m_thruster = null;
            }

            // we need to set our thruster to the one we now collided with, and get rid of it visually be reparenting it and scaling to 0
            m_thruster = collision.gameObject.GetComponent<ThrusterBase>();
            collision.transform.SetParent(this.transform);
            collision.transform.localScale = Vector3.zero;
        }
    }

    #endregion


    #region PLAYER FUNCTIONS

    /// <summary>
    /// deal with any and all input relevant to the palyer and trigger the appropriate functions on the right components
    /// </summary>
    private void process_input()
    {
        // forward the movement controls to possible thruster implementation
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_thruster?.forward(this);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_thruster?.left(this);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_thruster?.backward(this);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_thruster?.right(this);
        }

        // forward mouse input to the appropriate components
        if (Input.GetMouseButtonDown(0))    // LEFT
        {
            m_thruster?.leftclick(this);
            m_weapon?.shoot();
        }
        if (Input.GetMouseButtonDown(1))    // RIGHT
        {
            m_thruster?.rightclick(this);
        }
        if (Input.GetMouseButtonDown(2))    // MIDDLE
        {
            m_thruster?.middleclick(this);
        }
    }

    #endregion
}
