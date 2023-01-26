using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Screen")]
    public LoadingScreen Loading_Screen;
    public LobbyScreen Lobby_Screen;

    /// <summary>
    /// Deactive all Screen.
    /// </summary>
    public void DeactiveScreen()
    {
        Lobby_Screen.Hide();
        Loading_Screen.Hide();
    }

    /// <summary>
    /// Loading Screen With has Massage.
    /// </summary>
    /// <param name="Massage"></param>
    public void ShowLoading(string Massage)
    {
        DeactiveScreen();
        Loading_Screen.Show();
        Loading_Screen.Loading_Text.text = Massage;
    }

}
