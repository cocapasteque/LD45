using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> Enemies;

    private bool _spawning;
    private List<CraftingLevel> _levels;
    private PlayerScript _player;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _levels = CraftingSystem.Instance.Progress.Levels;
        Enemies = Enemies.OrderBy(x => x.level).ToList();
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
            yield return new WaitForSeconds(_levels[CraftingSystem.Instance.CurrentLevel].EnemySpawnCooldown);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        float rnd = Random.Range(0, 100);
        Enemy toSpawn = null;
        foreach (var level in _levels[CraftingSystem.Instance.CurrentLevel].EnemySpawnPercentages)
        {
            if (rnd <= level.Percentage)
            {
                toSpawn = Enemies[level.Index];
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
            GameObject go = Instantiate(toSpawn.prefab, _player.transform.position + spawnPos, Quaternion.identity);
            Vector3 startVelocity = GetStartVelocity(spawnPos);

            EnemyBehavior enemy = go.AddComponent(typeof(EnemyBehavior)) as EnemyBehavior;
            enemy.Init(Random.Range(toSpawn.hitPoints.x, toSpawn.hitPoints.y), Random.Range(toSpawn.aggroRange.x, toSpawn.aggroRange.y) * Vector3.Distance(spawnPos, _player.transform.position),
                Random.Range(toSpawn.acceleration.x, toSpawn.acceleration.y), startVelocity);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var frustumHeight = 2.0f * _camera.transform.position.y * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * _camera.aspect;
        var spawnRadius = Mathf.Sqrt(Mathf.Pow(frustumHeight, 2f) + Mathf.Pow(frustumWidth, 2f));
        spawnRadius *= Random.Range(1.5f, 2.0f);
        Vector2 rnd = Random.insideUnitCircle;
        rnd = rnd.normalized;
        Vector3 spawnPos = new Vector3(rnd.x * spawnRadius, 0f, rnd.y * spawnRadius);
        return spawnPos;
    }

    private Vector3 GetStartVelocity(Vector3 spawnPos)
    {
        float durationToPlayer = Random.Range(10, 15);
        Vector3 target = _player.transform.position + _player.GetComponent<Rigidbody>().velocity * durationToPlayer;
        Vector2 rnd = Vector2.zero;
        target = new Vector3(target.x + rnd.x, 0, target.z + rnd.y);
        Vector3 result = (target - spawnPos) / durationToPlayer;
        return result;
    }
}
