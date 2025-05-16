using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomsController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _roomButtonPrefab;
    [SerializeField] private Transform _roomListPanel;

    private List<RoomInfo> _rooms = new List<RoomInfo>();

    private void Start()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _rooms = roomList;
        UpdateRoomList();
    }

    private void UpdateRoomList()
    {
        foreach (Transform child in _roomListPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo room in _rooms)
        {
            if (room.RemovedFromList == false)
            {
                GameObject roomButton = Instantiate(_roomButtonPrefab, _roomListPanel);
                roomButton.GetComponent<RoomButton>().SetUp(room);
            }
        }
    }
}
