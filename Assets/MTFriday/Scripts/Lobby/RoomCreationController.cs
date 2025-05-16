using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomCreationController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_InputField _roomNameInputField;

    public void OnPointerClick(PointerEventData eventData)
    {
        string roomText = _roomNameInputField.text;

        if (string.IsNullOrWhiteSpace(roomText))
            return;

        PhotonNetwork.CreateRoom(roomText);
    }
}
