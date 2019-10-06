using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordGameManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer _fadePlane;
    [SerializeField] private float _fadeTime;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) FadeStartPlane();
        
    }

    public void FadeStartPlane()
    {
        StartCoroutine(Work());
        
        IEnumerator Work()
        {
            var block = new MaterialPropertyBlock();
            var t = 0f;
            while (t < 1f)
            {
                _fadePlane.GetPropertyBlock(block);
                block.SetColor("_BaseColor", Color.Lerp(Color.black, Color.clear, t));
                _fadePlane.SetPropertyBlock(block);
                t += Time.deltaTime / _fadeTime;
                yield return null;
            }
        }
    }
}
