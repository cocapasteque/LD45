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
    
    public static float Remap (this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}