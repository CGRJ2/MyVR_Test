using DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Customer : MonoBehaviour
{
    [SerializeField] int singleIcePrice;
    [SerializeField] float maxTimer;
    [SerializeField] float minTimer;
    Coroutine timerCorountine;

    CustomerManager cm;
    public Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;

    Vector3 leavePoint;
    public ObservableProperty<Vector3> TargetPoint = new();

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
        agent.SetDestination(TargetPoint.Value);
    }

    private void OnEnable()
    {
        TargetPoint.Subscribe(MoveToTarget);
    }
    private void OnDisable()
    {
        TargetPoint.UnsbscribeAll();
        StopCoroutine(timerCorountine);
    }

    // 타겟이 업데이트 되면 자동으로 이동 시작 (TargetPoint와 이벤트 연결)
    void MoveToTarget(Vector3 target)
    {
        if (agent != null)
            agent.SetDestination(target);
    }

    private void Update()
    {
        // 목적지에 도착한 상태
        if (ReachedDestination(agent))
        {
            // 목적지가 첫번째 줄이라면 && 주문 시작 전 상태라면
            if (agent.destination.x == cm.GetFirstPos().x
                && agent.destination.z == cm.GetFirstPos().z
                && !activateOrder)
            {
                ArriveFirst();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (orderStack == cm.NowOrder.Value)
            Leave();
        }
    }

    // 손님 생성 시, 만들어진 주문 데이터 저장 (cm에서 호출)
    public void SetOrder(Stack<IceCreamTasteType> order)
    {
        orderStack = order;
    }

    // navy mesh agent 타겟 설정 용도
    public void SetTargetPoint(Vector3 target)
    {
        TargetPoint.Value = target;
    }

    // navy mesh agent 목적지 도착 판별
    bool ReachedDestination(NavMeshAgent agent)
    {
        return !agent.pathPending                          // 아직 경로 계산 중 아님
            && agent.remainingDistance <= agent.stoppingDistance  // 거의 다 왔고
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f); // 멈췄다
    }

    // 첫번째 손님이 되었을 때 한 번만 호출
    public void ArriveFirst()
    {
        activateOrder = true; // 한 번만 호출하기 위한 플래그
        cm.SetNowOrder(orderStack);
        UIManager.Instance.ActivateOrderPanel();
        timerCorountine = StartCoroutine(TimerCoroutine());
    }

    public void SetRandomTimer()
    {
        timeLimit = UnityEngine.Random.Range(minTimer, maxTimer);
    }
    private IEnumerator TimerCoroutine()
    {
        SetRandomTimer();
        currentTime = 0;
        Debug.Log("타이머 시작");
        while (currentTime < timeLimit)
        {
            UIManager.Instance.SetTimerUI(timeLimit - currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("타이머 완료");

        // 타임 아웃 분노 <<<
        UIManager.Instance.SetTimerUI("Where is my ice cream?");
        yield return new WaitForSeconds(1f);
        TimeOut();
    }

    // 첫번째 손님 나가기
    // 만족 or 불만족 체크
    public void Leave()
    {
        agent.SetDestination(leavePoint);
        cm.RemoveFirstCustomer();
        UIManager.Instance.DeactivateOrderPanel();
    }

    public void ReceiveIceCream(SelectEnterEventArgs args)
    {
        Corn corn = args.interactableObject.transform.GetComponent<Corn>();
        PlayerManager pm = PlayerManager.Instance;

        if (corn.GetTasteStackData().SequenceEqual(orderStack))
        {
            // 명성과 돈 지급
            pm.GetMoney(corn.GetPrice(singleIcePrice));
            pm.GetFame(5);

            // 만족하는 애니메이션 실행
        }
        else
        {
            // 주문과 다른 아이스크림 수 만큼 명성 감소
            IceCreamTasteType[] orderArray = orderStack.ToArray();
            IceCreamTasteType[] myArray = corn.GetTasteStackData().ToArray();
            int wrongIceCount = 0;
            for (int i = 0; i < orderArray.Length; i++)
            {
                if (myArray.Length <= i)
                {
                    wrongIceCount += 1;
                    continue;
                }

                if (orderArray[i] != myArray[i]) wrongIceCount += 1;
            }

            // 돈 절반만 지급, 명성 감소
            pm.GetMoney(corn.GetPrice(singleIcePrice)/2);
            pm.GetFame(-wrongIceCount);


            // 불만족하는 애니메이션 실행
        }

        // 애니메이션 종료 후
        Leave();
    }


    public void TimeOut()
    {
        PlayerManager pm = PlayerManager.Instance;
        pm.GetFame(-5);
        Leave();
    }


    // 손님 타입 (온화, 보통, 심술)

    // 스택 랜덤 생성 함수 & 성격 랜덤
    // 손님 성격에 따라 시간 설정해주는 함수
    // 제한 시간 대비 남은 시간 비율에 따라 표정 변화 (단계별 변화 & 시계 UI에 반영)

    // TODO: 손님 떠나는 조건 구현

    

    

    private void OnTriggerEnter(Collider other)
    {
        // 이것도 오버랩 스피어로 바꿔야할듯.
        Debug.Log("아이스크림 받음");
    }
}
