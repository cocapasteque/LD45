/* 
 * Script that manages pretty much everything that has to do with the player, including:
 * - movement
 * - combat
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerScript : MonoBehaviour
{
    #region VARIABLES
    public List<Thruster> m_thrusters;
    public List<WeaponBase> m_weapons;

    // plane though the origin with world up vector, which represens the plane the game takes place on in top-down fashion
    private Plane m_game_plane;

    private bool m_shooting = false;
    private bool m_thrusting = false;

    private Coroutine m_shooting_coroutine;

    [Tooltip("the gameobject that represents the player in the scene, a.k.a. the naked guy")]
    [SerializeField]
    private GameObject m_player_mesh;

    [Tooltip("the camera that focuses on this player. is on same hierarchy level as the character model")]
    [SerializeField]
    private Camera m_camera;

    private float m_default_camera_distance;
    
    private Rigidbody m_rb;
    public Rigidbody RigidBody => m_rb;
    public GameObject Player_Mesh => m_player_mesh;

    private float _health;
    public float Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, 100);
    }


    [SerializeField] private Volume _postProcSpeed;
    [SerializeField] private AnimationCurve _fovCurve;
    [SerializeField] private float _maxSpeed;
    #endregion
    
    #region UNITY LIFECYCLE
    private void Awake()
    {
        Health = 100;
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        m_rb.useGravity = false; // make sure to have gravity off, we are in space

        if (m_camera == null)
        {
            m_camera = Camera.main;
        }

        // make the default game plane through origin with world up
        m_game_plane = new Plane(Vector3.up, Vector3.zero);

        if(m_weapons == null) m_weapons = new List<WeaponBase>();
        if(m_thrusters == null) m_thrusters = new List<Thruster>();
    }
    private void Start()
    {
        // give the player an initial push toward the thruster
        m_rb.AddForce(this.transform.forward * 0.0f);

        m_shooting_coroutine = StartCoroutine(Shoot());
        m_default_camera_distance = (m_camera.transform.position - m_player_mesh.transform.position).magnitude;
    }
    private void Update()
    {
        HandleRotation();
        HandleShooting();
        HandleThrusting();
        HandleCamera();
    }
    #endregion

    #region PLAYER FUNCTIONS
    private IEnumerator Shoot()
    {
        // dear lord have mercy, we are indenting all the way to hell now...get ready
        while (true)
        {
            if (m_shooting)
            {
                foreach (var weapon in m_weapons.Where(weapon => !weapon.On_Cooldown))
                {
                    weapon.Shoot(m_rb.velocity.magnitude);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
    private void HandleRotation()
    { 
        var pos = Utils.GetMousePositionOn2DPlane();
        if(pos.HasValue)
            m_player_mesh.transform.LookAt(pos.Value);
    }
    private void HandleShooting()
    {
        if (LordGameManager.Instance.UIOpened) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            m_shooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_shooting = false;
        }
    }
    private void HandleThrusting()
    {
        if (!Input.GetMouseButton(1)) return;
        
        foreach (var thruster in m_thrusters)
        {
            m_rb.AddForce(Time.deltaTime * thruster.Thrust_Strength * m_player_mesh.transform.forward);
        }

        m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, _maxSpeed);
    }
    private void HandleCamera()
    {
        Debug.Log(m_rb.velocity.magnitude);
        // the faster the player is, the further away the camera is from him
        m_camera.transform.position = m_player_mesh.transform.position + Vector3.up*(m_default_camera_distance + m_rb.velocity.magnitude);
        _postProcSpeed.weight = m_rb.velocity.magnitude.Remap(0, 10, 0, 1);
        m_camera.fieldOfView = m_rb.velocity.magnitude.Remap(0,10,40,150);
    }


    public void AddThruster(Thruster thruster)
    {
        var created = Instantiate(thruster.gameObject, this.transform, true);
        m_thrusters.Add(created.GetComponent<Thruster>());
        created.transform.localScale = Vector3.zero;
    }
    public void TakeDamages(float val)
    {
        Health -= val;
    }
    public void Heal(float val)
    {
        Health += val;
    }
    #endregion
}
