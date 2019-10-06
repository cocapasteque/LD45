using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceSpawner : MonoBehaviour
{
    public List<GameItem> Resources;
    public Vector2 RotationSpeed;
    public Vector2 MovementSpeed;

    private Camera _camera;
    private bool _spawning;
    private List<CraftingLevel> _levels;
    private PlayerScript _player;

    void Start()
    {
        _camera = Camera.main;
        _levels = CraftingSystem.Instance.Progress.Levels;
        Resources = Resources.OrderBy(x => x.CraftingValue).ToList();
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
            yield return new WaitForSeconds(_levels[CraftingSystem.Instance.CurrentLevel].SpawnCooldown);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        float rnd = Random.Range(0, 100);
        GameItem toSpawn = null;
        for(int i = 0; i < _levels[CraftingSystem.Instance.CurrentLevel].ResourceSpawnPercentages.Count; i++)
        {
            if (rnd <= _levels[CraftingSystem.Instance.CurrentLevel].ResourceSpawnPercentages[i].Percentage)
            {
                toSpawn = Resources[_levels[CraftingSystem.Instance.CurrentLevel].ResourceSpawnPercentages[i].Index];
                break;
            }
            else
            {
                rnd -= _levels[CraftingSystem.Instance.CurrentLevel].ResourceSpawnPercentages[i].Percentage;
            }
        }
        Vector3 spawnPos = GetSpawnPosition();
        Quaternion quaternion = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        GameObject go = Instantiate(toSpawn.prefab, _player.transform.position + spawnPos, quaternion);
        Vector3 trajectory = GetTrajectory(_player.transform.position + spawnPos);
        Vector3 rotationSpeed = GetRotationSpeed();
        CollectableScrap scrap = go.AddComponent(typeof(CollectableScrap)) as CollectableScrap;
        scrap.Init(trajectory, rotationSpeed, toSpawn);
    }  

    private Vector3 GetSpawnPosition()
    {
        var frustumHeight = 2.0f * _camera.transform.position.y * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * _camera.aspect;
        var spawnRadius = Mathf.Sqrt(Mathf.Pow(frustumHeight, 2f) + Mathf.Pow(frustumWidth, 2f));
        spawnRadius *= Random.Range(1.1f, 1.3f);
        Vector2 rnd = Random.insideUnitCircle;
        Vector3 spawnPos = new Vector3(rnd.x * spawnRadius, 0f, rnd.y * spawnRadius);
        return spawnPos;
    }

    //TODO REDO!!!
    private Vector3 GetTrajectory(Vector3 spawnPos)
    {
        Vector3 dir = _player.transform.position + _player.GetComponent<Rigidbody>().velocity - spawnPos;
        Vector3 result = dir * Random.Range(MovementSpeed.x, MovementSpeed.y);
        result = new Vector3(result.x, 0, result.z);
        return result;
    }

    private Vector3 GetRotationSpeed()
    {
        Vector3 angle = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Vector3 result = angle.normalized * Random.Range(RotationSpeed.x, RotationSpeed.y);
        return result;
    }
}
