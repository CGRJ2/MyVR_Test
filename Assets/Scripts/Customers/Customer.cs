using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;

    Vector3 targetPoint;


    private void Start()
    {
        // 생성 시, 아이스크림 판매 대로 이동
        

        // 도착하면 UI 반영(UIManager 호출)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IceCreamTasteType[] a = orderStack.ToArray();
            foreach (IceCreamTasteType aa in a)
            {
                Debug.Log(aa);
            }
        }
    }

    public void InitCustomer()
    {

    }

    public void SetOrder(Stack<IceCreamTasteType> order)
    {
        orderStack = order;
    }
    
    public void SetTargetPoint(Vector3 target)
    {
        targetPoint = target;
        // 임시
        transform.position = targetPoint;
    }

    // 손님 타입 (온화, 보통, 심술)

    // 스택 랜덤 생성 함수 & 성격 랜덤
    // 손님 성격에 따라 시간 설정해주는 함수
    // 제한 시간 대비 남은 시간 비율에 따라 표정 변화 (단계별 변화 & 시계 UI에 반영)

    // TODO: 손님 복귀 시, 줄 상태 업데이트 (한 칸씩 앞으로 이동 & 다음 1번 손님 업데이트)
}
