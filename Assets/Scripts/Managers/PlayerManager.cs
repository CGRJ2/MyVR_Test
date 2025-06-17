using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] int money;
    [SerializeField] int fame;

    public void Init()
    {
        base.SingletonInit();
    }

    public void GetMoney(int money)
    {
        this.money += money;
        UIManager.Instance.UpdateUpperUI(this.fame.ToString() , this.money.ToString());
    }

    public void GetFame(int fame)
    {
        this.fame += fame;
        UIManager.Instance.UpdateUpperUI(this.fame.ToString(), this.money.ToString());
    }

}
