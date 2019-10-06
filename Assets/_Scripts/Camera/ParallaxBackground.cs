using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEditor;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _backgroundObject;
    private SpriteRenderer _currentBackground;
    [SerializeField] private LayerMask _backgroundLayer;

    [SerializeField] private float _debugLength = 50f;
    [SerializeField] private float _extent = .25f;

    private void Update()
    {
        GetBackground();
        CheckSides();
        HandleOutOfSightBackground();
    }

    private void GetBackground()
    {
        Ray centerRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(centerRay, out hit, 100, _backgroundLayer))
        {
            _currentBackground = hit.collider.GetComponent<SpriteRenderer>();
        }
    }

    private void CheckSides()
    {
        var leftRay = _camera.ViewportPointToRay(new Vector3(0f - _extent, 0.5f));
        var rightRay = _camera.ViewportPointToRay(new Vector3(1f + _extent, 0.5f));
        var topRay = _camera.ViewportPointToRay(new Vector3(0.5f, 1f + _extent));
        var bottomRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0f - _extent));

        var topRight = _camera.ViewportPointToRay(new Vector3(1f + _extent, 1f+ _extent));
        var topLeft = _camera.ViewportPointToRay(new Vector3(0f - _extent, 1f + _extent));
        var botRight = _camera.ViewportPointToRay(new Vector3(1f + _extent, 0f - _extent));
        var botLeft = _camera.ViewportPointToRay(new Vector3(0f - _extent, 0f - _extent));

        Debug.DrawRay(leftRay.origin, leftRay.direction * _debugLength, Color.red);
        Debug.DrawRay(rightRay.origin, rightRay.direction * _debugLength, Color.red);
        Debug.DrawRay(topRay.origin, topRay.direction * _debugLength, Color.red);
        Debug.DrawRay(bottomRay.origin, bottomRay.direction * _debugLength, Color.red);

        Debug.DrawRay(topRight.origin, topRight.direction * _debugLength, Color.red);
        Debug.DrawRay(topLeft.origin, topLeft.direction * _debugLength, Color.red);
        Debug.DrawRay(botRight.origin, botRight.direction * _debugLength, Color.red);
        Debug.DrawRay(botLeft.origin, botLeft.direction * _debugLength, Color.red);

        var bounds = _currentBackground.bounds;

        // If not raycast to background, we need to add one.
        if (!Physics.Raycast(leftRay, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x - 2 * bounds.extents.x, bounds.center.y, bounds.center.z);
        }

        if (!Physics.Raycast(rightRay, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x + 2 * bounds.extents.x, bounds.center.y, bounds.center.z);
        }

        if (!Physics.Raycast(topRay, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x, bounds.center.y, bounds.center.z + 2 * bounds.extents.z);
        }

        if (!Physics.Raycast(bottomRay, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x, bounds.center.y, bounds.center.z - 2 * bounds.extents.z);
        }

        if (!Physics.Raycast(topRight, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x + 2 * bounds.extents.x, bounds.center.y,
                    bounds.center.z + 2 * bounds.extents.z);
        }

        if (!Physics.Raycast(topLeft, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x - 2 * bounds.extents.x, bounds.center.y,
                    bounds.center.z + 2 * bounds.extents.z);        }

        if (!Physics.Raycast(botRight, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x + 2 * bounds.extents.x, bounds.center.y,
                    bounds.center.z - 2 * bounds.extents.z);        }

        if (!Physics.Raycast(botLeft, out _, 100, _backgroundLayer))
        {
            var background = Instantiate(_backgroundObject, transform);
            background.transform.position =
                new Vector3(bounds.center.x - 2 * bounds.extents.x, bounds.center.y,
                    bounds.center.z - 2 * bounds.extents.z);        }
    }

    private void HandleOutOfSightBackground()
    {
    }
}