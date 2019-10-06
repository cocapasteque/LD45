using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthFinder : MonoBehaviour
{
    public RectTransform Arrow;

    private Transform player;
    private Transform earth;
    private Vector3 direction;
   
    // Start is called before the first frame update
    void Awake()
    {
        CanvasScaler c = GetComponentInParent<CanvasScaler>();
        c.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        c.referenceResolution = new Vector2(1920, 1080);
        c.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        player = FindObjectOfType<PlayerScript>().transform;
        earth = FindObjectOfType<Planet>().transform;
    }

    void Update()
    {
        direction = earth.position - player.position;
        direction = new Vector3(direction.x, 0, direction.z);
        float angle = Vector3.SignedAngle(direction, Vector3.forward, Vector3.up);
        Arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
