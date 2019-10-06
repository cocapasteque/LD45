using System;
using UnityEngine;

public static class Utils
{
    public static Vector3? GetMousePositionOn2DPlane()
    {
        if (Camera.main == null) throw new NullReferenceException("Main camera is null.");
        
        var gamePlane = new Plane(Vector3.up, Vector3.zero);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!gamePlane.Raycast(ray, out var distance)) return null;
        var hit = ray.GetPoint(distance);
        return hit;
    }
}