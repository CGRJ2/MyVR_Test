using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CustomerManager customerManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerManager playerManager;

    // 게임 상태 : 장사 대기 > 장사 시작 > 장사 종료 > 정산 > 장사 대기 ... ////// +일시정지
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

    // 게임매니저 제외한 싱글톤 객체들 초기화 순서 세팅
    private void InitalizeOrderSetting()
    {
        // 초기화할 순서대로 나열
        playerManager.Init();
        customerManager.Init();
        uiManager.Init();
    }

}
public enum IceCreamTasteType
{
    Strawberry, Chocolate, Vanilla
}