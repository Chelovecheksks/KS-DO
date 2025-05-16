using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _totalItemsCollected;

    private void Start()
    {
        PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        CheckItems();
    }

    private void CheckItems()
    {
        if (_totalItemsCollected < 0)
            _totalItemsCollected = 0;
    }

    public void AddItem()
    {
        _totalItemsCollected++;
    }

    public void TakeAwayItems(int count)
    {
        _totalItemsCollected -= count;
    }
}
