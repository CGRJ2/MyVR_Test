using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    CustomerManager cm;

    public Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;

    Vector3 leavePoint;
    Vector3 targetPoint;
    public NavMeshAgent agent;
    bool activateOrder = false;

    private void Start()
    {
        // 임시 ///////////////////////
        leavePoint = new Vector3(8, 0, 8);
        ///////////////////////////////


        cm = CustomerManager.Instance;

        // 생성 시, 아이스크림 판매 대로 이동
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint);

        // 도착하면 UI 반영(UIManager 호출)
    }

    private void Update()
    {
        if (ReachedDestination(agent))
        {
            // 첫번째 줄에 도착 && 주문 시작 전 상태라면
            if (agent.destination.x == cm.GetFirstPos().x
                && agent.destination.z == cm.GetFirstPos().z
                && !activateOrder)
            {
                ArriveFirst();
            }
        }

        // 테스트 용도
        if (Input.GetKeyDown(KeyCode.G))
        {
            IceCreamTasteType[] a = orderStack.ToArray();
            foreach (IceCreamTasteType aa in a)
            {
                Debug.Log(aa);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (orderStack == cm.NowOrder.Value)
            Leave();
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
    }

    bool ReachedDestination(NavMeshAgent agent)
    {
        return !agent.pathPending                          // 아직 경로 계산 중 아님
            && agent.remainingDistance <= agent.stoppingDistance  // 거의 다 왔고
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f); // 멈췄다
    }

    public void ArriveFirst()
    {
        activateOrder = true;
        cm.SetNowOrder(orderStack);
        UIManager.Instance.ActivateOrderPanel();
    }

    // 첫번째 손님 나가기
    // 만족 or 불만족 체크
    public void Leave()
    {
        agent.SetDestination(leavePoint);
        cm.RemoveFirstCustomer();
        UIManager.Instance.DeactivateOrderPanel();
    }


    // 손님 타입 (온화, 보통, 심술)

    // 스택 랜덤 생성 함수 & 성격 랜덤
    // 손님 성격에 따라 시간 설정해주는 함수
    // 제한 시간 대비 남은 시간 비율에 따라 표정 변화 (단계별 변화 & 시계 UI에 반영)

    // TODO: 손님 복귀 시, 줄 상태 업데이트 (한 칸씩 앞으로 이동 & 다음 1번 손님 업데이트)



    private void OnTriggerEnter(Collider other)
    {
        // 이것도 오버랩 스피어로 바꿔야할듯.
        Debug.Log("아이스크림 받음");
    }
}
