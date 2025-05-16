using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3f;
    private int _itemsCollected;
    private PhotonView _photonView;
    private GameManager _gameManager;
    private GameObject _playerInstance;
    [SerializeField] private Transform _spawner;

    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>().transform;
        _photonView = GetComponent<PhotonView>();
        _gameManager = FindObjectOfType<GameManager>();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (_photonView.IsMine)
        {
            _playerInstance = PhotonNetwork.Instantiate("PlayerController", _spawner.position, Quaternion.identity);
            PlayerController player = _playerInstance.GetComponent<PlayerController>();
        }
    }

    public void KillPlayer()
    {
        PhotonNetwork.Destroy(_playerInstance);
        _gameManager.TakeAwayItems(_itemsCollected);
        _itemsCollected = 0;
        StartCoroutine("SpawnPlayerBeetwenTime");
    }

    IEnumerator SpawnPlayerBeetwenTime()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnPlayer();
    }

    public void CollectItem()
    {
        _itemsCollected++;
        _gameManager.AddItem();
    }
}
