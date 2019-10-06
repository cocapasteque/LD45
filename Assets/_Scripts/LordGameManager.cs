using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Serialization;

public class LordGameManager : MonoBehaviour
{
    public static LordGameManager Instance = null;
    
    [SerializeField] private MeshRenderer _fadePlane;
    [SerializeField] private float _fadeTime;

    public bool UIOpened = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private IEnumerator Start()
    {
        // Setting fade plane to black at start
        var block = new MaterialPropertyBlock();
        _fadePlane.GetPropertyBlock(block);
        block.SetColor("_BaseColor", Color.black);
        _fadePlane.SetPropertyBlock(block);

        yield return new WaitForSeconds(3);
        FadeStartPlane();
    }

    private void Update()
    {
        // Inventory open/close logic
        if (!UIOpened && (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.I)))
        {
            UIOpened = true;
            GameEventMessage.SendEvent("OpenInventory");
        }
        else if (UIOpened && (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.I)))
        {
            UIOpened = false;
            GameEventMessage.SendEvent("CloseInventory");
        }
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
