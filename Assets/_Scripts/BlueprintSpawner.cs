﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlueprintSpawner : MonoBehaviour
{
    public List<Blueprint> Blueprints;
    public Vector2 RotationSpeed;
    public Vector2 MovementTimeToPlayer;
    public GameObject BlueprintPrefab;

    private Camera _camera;
    private bool _spawning;
    private List<CraftingLevel> _levels;
    private PlayerScript _player;

    void Start()
    {
        _camera = Camera.main;
        _levels = CraftingSystem.Instance.Progress.Levels;
        Blueprints = Blueprints.OrderBy(x => x.level).ToList();
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
            yield return new WaitForSeconds(_levels[CraftingSystem.Instance.CurrentLevel].BlueprintSpawnCooldown);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        float rnd = Random.Range(0, 100);
        Blueprint toSpawn = null;
        foreach (var level in _levels[CraftingSystem.Instance.CurrentLevel].BlueprintSpawnPercentages)
        {
            if (rnd <= level.Percentage)
            {
                if (!CraftingSystem.Instance.unlockedBlueprints[Blueprints[level.Index]])
                {
                    toSpawn = Blueprints[level.Index];
                }
                break;
            }
            else
            {
                rnd -= level.Percentage;
            }
        }
        if (toSpawn != null)
        {
            Vector3 spawnPos = GetSpawnPosition();
            Quaternion quaternion = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject go = Instantiate(BlueprintPrefab, _player.transform.position + spawnPos, quaternion);
            Vector3 trajectory = GetTrajectory(_player.transform.position + spawnPos, true);
            Vector3 rotationSpeed = GetRotationSpeed();

            CollectableBlueprint bp = go.AddComponent(typeof(CollectableBlueprint)) as CollectableBlueprint;
            bp.Init(trajectory, rotationSpeed, toSpawn);
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

    private Vector3 GetTrajectory(Vector3 spawnPos, bool directlyTowardsPlayer = false)
    {
        float durationToPlayer = Random.Range(MovementTimeToPlayer.x, MovementTimeToPlayer.y);
        Vector3 target = _player.transform.position + _player.GetComponent<Rigidbody>().velocity * durationToPlayer;
        Vector2 rnd = Vector2.zero;
        if (!directlyTowardsPlayer)
        {
            rnd = Random.insideUnitCircle * Mathf.Clamp(_player.GetComponent<Rigidbody>().velocity.magnitude * 0.01f, 5, 100);
        }
        target = new Vector3(target.x + rnd.x, 0, target.z + rnd.y);
        Vector3 result = (target - spawnPos) / durationToPlayer;
        return result;
    }

    private Vector3 GetRotationSpeed()
    {
        Vector3 angle = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Vector3 result = angle.normalized * Random.Range(RotationSpeed.x, RotationSpeed.y);
        return result;
    }
}
