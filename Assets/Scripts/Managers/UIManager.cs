using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] OrderPanel orderPanel;
    [SerializeField] TMP_Text fameTMP;
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text timer;

    
    public void Init()
    {
        base.SingletonInit();

        orderPanel.Init();
    }

    public void SetTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds); // 01:23
        timer.text = timeText;
    }

    public void SetTimerUI(string timeOutMessage)
    {
        timer.text = timeOutMessage;
    }

    public void UpdateUpperUI(string fame, string money)
    {
        fameTMP.text = fame;
        moneyTMP.text = money;
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
