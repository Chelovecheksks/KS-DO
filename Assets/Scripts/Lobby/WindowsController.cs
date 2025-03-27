using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private List<Window> _windows; 
    public static WindowsController Menu { get; private set; }

    public enum WindowType
    {
        Load = 0,
        RoomMenu = 1,
        CreateRoom = 2,
        Room = 3,
        FindRoom = 4,
        Quit = 5,
        ChooseNickName = 6,
    }

    private void Start()
    {
        Menu = this;
    }

    public void ChangeWindow(WindowType windowType)
    {
        foreach (Window window in _windows)
        {
            if (window.WindowType == windowType)
                window.gameObject.SetActive(true);
            else
                window.gameObject.SetActive(false);
        }

        if (windowType == WindowType.Quit)
            Application.Quit();
    }
}
