using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("RPC Event")]
    private PhotonView photonView;

    #region Unity Methods...............

    private void Awake()
    {
        StartCoroutine(NickName_Scrren());
        photonView = GetComponent<PhotonView>();

        //Subscribe The ServerConnect Function.
        LobbyScreen.ConnectServer_Callback += ConnectToPhotonServer;
        LobbyScreen.JoinRandomRoom_Callback += JoinRandomRoom;
    }

    #endregion

    #region Privet Methods..............................

    /// <summary>
    /// Set The LocalPlayer NickName.
    /// </summary>
    private IEnumerator NickName_Scrren()
    {
        UIManager.Instance.ShowLoading("Loading..");
        yield return new WaitForSeconds(1f);
        UIManager.Instance.DeactiveScreen();
        UIManager.Instance.Lobby_Screen.Show();
        UIManager.Instance.Lobby_Screen.NickName.SetActive(true);
    }

    /// <summary>
    /// Connect to The Photon Server.
    /// </summary>
    private void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            UIManager.Instance.Lobby_Screen.NickName.SetActive(false);
            UIManager.Instance.ShowLoading("Connecting To Server..");
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();

            //DeSubscripbe The Function.
            LobbyScreen.ConnectServer_Callback -= ConnectToPhotonServer;
        }
    }


    /// <summary>
    /// Join Server RandomRoom.
    /// </summary>
    private void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// Crate The Room and After Join The Room.
    /// </summary>
    private void CrateAndJoinRoom()
    {
        string RandomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(RandomRoomName, roomOptions);

    }

    /// <summary>
    /// Load The GamePlay Scene.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartGamePlay()
    {
        yield return new WaitForSeconds(2f);

        if (PhotonNetwork.IsMasterClient)
        {
            UIManager.Instance.ShowLoading("Connect To The GamePlay..");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("GamePlay");
        }

    }

    #endregion


    #region Photon Callbacks..........

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Connected To server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for resone" + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        UIManager.Instance.DeactiveScreen();
        UIManager.Instance.Lobby_Screen.Show();
        UIManager.Instance.Lobby_Screen.MatchMacking_Screen.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CrateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.ShowLoading("Wait For Other Player..");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
        UIManager.Instance.ShowLoading("Wait For Other Player..");

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (photonView.IsMine)
            {
                photonView.RPC(nameof(GameStart_Synce), RpcTarget.All);
            }
        }
    }

    #endregion

    #region Photon Rise & RPC Events ......................

    [PunRPC]
    private void GameStart_Synce()
    {
        StartCoroutine(StartGamePlay());
    }

    #endregion
}
