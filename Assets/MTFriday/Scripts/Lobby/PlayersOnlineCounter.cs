using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayersOnlineCounter : MonoBehaviour
{
    private TextMeshProUGUI _playersOnlineCounterText;

    private IEnumerator RefreshPlayersOnline()
    {
        while(true)
        {
            _playersOnlineCounterText.text = "Players Online: " + PhotonNetwork.CountOfPlayers;
            yield return new WaitForSeconds(5.1f);
        }
    }

    private void OnEnable()
    {
        _playersOnlineCounterText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(RefreshPlayersOnline());
    }
}
