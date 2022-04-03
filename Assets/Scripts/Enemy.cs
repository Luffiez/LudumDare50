using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/New Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public GameObject prefab;
    [Range(0f,1f)]
    public float spawnChance = 0.5f;
}