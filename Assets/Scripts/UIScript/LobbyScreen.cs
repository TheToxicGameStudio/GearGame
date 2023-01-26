using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class LobbyScreen : BaseView
{
    //Unity Events.......
    public static event Action ConnectServer_Callback;
    public static event Action JoinRandomRoom_Callback;

    [Header("Setup The NickName")]
    public GameObject NickName;
    public GameObject MatchMacking_Screen;
    [SerializeField] private TMP_InputField NickName_Input;

    private void OnEnable()
    {
        DeactiveScreen();
    }

    /// <summary>
    /// Deactive All Screen.
    /// </summary>
    private void DeactiveScreen()
    {
        NickName.SetActive(false);
        MatchMacking_Screen.SetActive(false);
    }

    /// <summary>
    /// Set User Inputed NickName.
    /// </summary>
    public void SetNickName()
    {
        string PlayerName = NickName_Input.text;
        if (!string.IsNullOrEmpty(PlayerName))
        {
            PhotonNetwork.LocalPlayer.NickName = PlayerName;
            ConnectServer_Callback.Invoke();
        }
        else
            Debug.Log("PlayerName is Invalid!");
    }

    /// <summary>
    /// Join The Photon RandomRoom.
    /// </summary>
    public void JoinRandomRoom()
    {
        JoinRandomRoom_Callback.Invoke();
    }

    /// <summary>
    /// Exit The Game.
    /// </summary>
    public void Exit_GamePlay()
    {
        Application.Quit();
    }

}
