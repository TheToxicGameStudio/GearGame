using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("PhotonVersion")]
    private string Game_Version = "0.1";

    private void OnEnable()
    {
        UIManager.Instance.DeactiveScreen();
        UIManager.Instance.Loading_Screen.Loading_Text.text = "Connecting To Server..";
        PhotonNetwork.GameVersion = Game_Version;
        PhotonNetwork.ConnectUsingSettings();
    }

    #region Photon Methods..........

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from server for resone" + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        UIManager.Instance.Loading_Screen.Hide();
        UIManager.Instance.Lobby_Screen.Show();
    }

    #endregion
}
