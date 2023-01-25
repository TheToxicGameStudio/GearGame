using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Screen")]
    public LoadingScreen Loading_Screen;
    public LobbyScreen Lobby_Screen;

    public void DeactiveScreen()
    {
        Lobby_Screen.Hide();
        Loading_Screen.Show();
    }

}
