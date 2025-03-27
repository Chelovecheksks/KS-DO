using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseNickname : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_InputField _nickNameInputField;

    public void OnPointerClick(PointerEventData eventData)
    {
        string nickname = _nickNameInputField.text;

        if (string.IsNullOrWhiteSpace(nickname) || nickname.Length < 4)
            nickname = "Player" + Random.Range(10000, 99999);

        PhotonNetwork.NickName = nickname;
        WindowsController.Menu.ChangeWindow(WindowsController.WindowType.RoomMenu);
    }
}
