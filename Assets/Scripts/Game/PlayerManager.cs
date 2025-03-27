using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;
    private GameObject _playerInstance;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (_photonView.IsMine)
        {
            Transform spawnPoint = SpawnManager.instance.GetRandomSpawnPoint();
            _playerInstance = PhotonNetwork.Instantiate("PlayerController", spawnPoint.position, spawnPoint.rotation);
            PlayerController player = _playerInstance.GetComponent<PlayerController>();
            player.OnHealthChange += CheckPlayerHealth;
        }
    }

    private void KillPlayer()
    {
        PhotonNetwork.Destroy(_playerInstance);
        SpawnPlayer();
    }

    private void CheckPlayerHealth(float health)
    {
        if (health <= 0)
        {
            KillPlayer();
        }
    }
}
