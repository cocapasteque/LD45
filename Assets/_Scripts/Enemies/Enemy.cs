using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class Enemy : ScriptableObject
{
    public GameObject prefab;
    public int level;
    public Vector2 hitPoints;
    public Vector2 acceleration;
    [Tooltip("In relation to spawn distance")]
    public Vector2 aggroRange;
    public WeaponBase weapon;
}
