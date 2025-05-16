using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private int _maxCountOfEnemies;
    private int _currentEnemyCount;

    private void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        if (_currentEnemyCount < _maxCountOfEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {

        _currentEnemyCount++;
    }

    public void KillEnemy(GameObject enemyToKill)
    {
        PhotonNetwork.Destroy(enemyToKill);
        _currentEnemyCount--;
    }
}
