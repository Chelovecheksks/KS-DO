using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOpenWindow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WindowsController.WindowType _windowType;

    public void OnPointerClick(PointerEventData eventData)
    {
        WindowsController.Menu.ChangeWindow(_windowType);
    }
}
