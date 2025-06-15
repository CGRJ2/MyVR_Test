using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CustomerManager customerManager;

    // ���� ���� : ��� ��� > ��� ���� > ��� ���� > ���� > ��� ��� ... ////// +�Ͻ�����
    private void Awake() => Init();

    private void Init()
    {
        base.SingletonInit();
        InitalizeOrderSetting();
    }

    // ���ӸŴ��� ������ �̱��� ��ü�� �ʱ�ȭ ���� ����
    private void InitalizeOrderSetting()
    {
        // �ʱ�ȭ�� ������� ����
        customerManager.Init();
    }

}
public enum IceCreamTasteType
{
    Strawberry, Chocolate, Vanilla
}