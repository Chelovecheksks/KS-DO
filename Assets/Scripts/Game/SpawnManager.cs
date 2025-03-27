using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private Transform[] _spawnPoints;

    private void Awake()
    {
        instance = this;
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    public Transform GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}
