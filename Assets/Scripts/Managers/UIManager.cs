using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] OrderPanel orderPanel;

    public void Init()
    {
        base.SingletonInit();
    }

    public void ActivateOrderPanel()
    {
        orderPanel.gameObject.SetActive(true);
    }

    public void DeactivateOrderPanel()
    {
        orderPanel.gameObject.SetActive(false);
    }
}
