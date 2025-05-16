using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        WindowsController.Menu.ChangeWindow(WindowsController.WindowType.Load);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to lobby");
        if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))
            WindowsController.Menu.ChangeWindow(WindowsController.WindowType.ChooseNickName);
        else
            WindowsController.Menu.ChangeWindow(WindowsController.WindowType.RoomMenu);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        WindowsController.Menu.ChangeWindow(WindowsController.WindowType.Room);
    }
}
