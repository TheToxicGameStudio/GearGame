using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{

    /// <summary>
    /// Show The Gameobject
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide The Gameobject
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
