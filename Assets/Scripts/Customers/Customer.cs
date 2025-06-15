using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerManager cm;

    Stack<IceCreamTasteType> orderStack = new Stack<IceCreamTasteType>();
    float timeLimit;
    float currentTime;


    private void Start()
    {
        cm = CustomerManager.Instance;

        // 생성 시, 아이스크림 판매 대로 이동

    }

    public void InitCustomer()
    {
        SetCustomerDatas();
        //UI 반영
    }

    public void SetCustomerDatas()
    {
        orderStack = cm.RandomOrderStackGenerate();

        // 시간제한도 타입에 따라 랜덤 설정으로 추가
    }
    
    // 손님 타입 (온화, 보통, 심술)

    // 스택 랜덤 생성 함수 & 성격 랜덤
    // 손님 성격에 따라 시간 설정해주는 함수
    // 제한 시간 대비 남은 시간 비율에 따라 표정 변화 (단계별 변화 & 시계 UI에 반영)
}
