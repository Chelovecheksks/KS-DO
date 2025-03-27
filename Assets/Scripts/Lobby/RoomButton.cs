using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using TMPro;

public class RoomButton : MonoBehaviour, IPointerClickHandler
{
    private string _roomName;
    private RoomInfo _roomInfo;
    private TextMeshProUGUI _buttonText;

    public void OnPointerClick(PointerEventData eventData)
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);
    }

    public void SetUp(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        _roomName = roomInfo.Name;
    }

    private void Start()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonText.text = _roomName;
    }
}
