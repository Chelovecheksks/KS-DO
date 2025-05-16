using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private Transform _playersListPanel;
    [SerializeField] private GameObject _playerTextPrefab;
    [SerializeField] private GameObject _startButton;

    private List<Player> _players;

    public override void OnEnable()
    {
        base.OnEnable();
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        _players = new List<Player>(PhotonNetwork.PlayerList);
        UpdatePlayerList();

        if (PhotonNetwork.IsMasterClient)
            _startButton.SetActive(true);
        else
            _startButton.SetActive(false);
    }

    private void UpdatePlayerList()
    {
        foreach (Transform child in _playersListPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Player player in _players)
        {
            GameObject playerText = Instantiate(_playerTextPrefab, _playersListPanel);
            playerText.GetComponent<TextMeshProUGUI>().text = player.NickName;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _players.Add(newPlayer);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _players.Remove(otherPlayer);
        UpdatePlayerList();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _startButton.SetActive(true);
        }
    }
}
