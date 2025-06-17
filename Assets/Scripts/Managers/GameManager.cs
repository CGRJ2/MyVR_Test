using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CustomerManager customerManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerManager playerManager;

    // ���� ���� : ��� ��� > ��� ���� > ��� ���� > ���� > ��� ��� ... ////// +�Ͻ�����
    private void Awake() => Init();

    private void Init()
    {
        base.SingletonInit();
        InitalizeOrderSetting();
    }

    private void DayStart()
    {

    }

    private void DayOff()
    {

    }

    // ���ӸŴ��� ������ �̱��� ��ü�� �ʱ�ȭ ���� ����
    private void InitalizeOrderSetting()
    {
        // �ʱ�ȭ�� ������� ����
        playerManager.Init();
        customerManager.Init();
        uiManager.Init();
    }

}
public enum IceCreamTasteType
{
    Strawberry, Chocolate, Vanilla
}