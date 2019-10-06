using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RockSpawner : MonoBehaviour
{
    public List<GameObject> Rocks;

    private Camera _camera;
    private bool _spawning;
    private PlayerScript _player;

    void Start()
    {
        _camera = Camera.main;
        _player = FindObjectOfType<PlayerScript>();
        StartSpawning();
    }

    public void StartSpawning()
    {
        _spawning = true;
        StartCoroutine(Spawning());
    }

    public void StopSpawning()
    {
        _spawning = false;
    }

    private IEnumerator Spawning()
    {
        while (_spawning)
        {
            yield return new WaitForSeconds(1);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        int rnd = Random.Range(0, Rocks.Count - 1);
        GameObject toSpawn = Rocks[rnd];
        
        if (toSpawn != null)
        {
            Vector3 spawnPos = GetSpawnPosition();
            Quaternion quaternion = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject go = Instantiate(toSpawn, _player.transform.position + spawnPos, quaternion);
            Destroy(go, 30);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var frustumHeight = 2.0f * _camera.transform.position.y * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * _camera.aspect;
        var spawnRadius = Mathf.Sqrt(Mathf.Pow(frustumHeight, 2f) + Mathf.Pow(frustumWidth, 2f));
        spawnRadius *= Random.Range(1.1f, 1.2f);
        Vector2 rnd = Random.insideUnitCircle;
        rnd = rnd.normalized;
        Vector3 spawnPos = new Vector3(rnd.x * spawnRadius, 0f, rnd.y * spawnRadius);
        return spawnPos;
    }
}
