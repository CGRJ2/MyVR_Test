using DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    // 손님 랜덤 스폰 루틴

    PlayerManager pm;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform defaultTarget;
    [SerializeField] Transform npcSpawnPoint;
    [SerializeField] int maxIceCreamStackCount;
    [SerializeField] int maxCustomerCount;
    [SerializeField] float lineDistance;
    [SerializeField] float baseMinDelay;
    [SerializeField] float baseMaxDelay;
    [SerializeField] float minCap; // 명성치 최대 일 때 min
    [SerializeField] float maxCap; // 명성치 최대 일 때 max

    Coroutine startSellingCoroutine;

    

    Queue<Customer> customers = new Queue<Customer>();

    public ObservableProperty<Stack<IceCreamTasteType>> NowOrder { get; private set; } = new();

    public void Init()
    {
        base.SingletonInit();
        pm = PlayerManager.Instance;
    }

    private void OnDestroy()
    {
        NowOrder.UnsbscribeAll();

        if (startSellingCoroutine != null)
            StopCoroutine(startSellingCoroutine);
    }

    private void Start()
    {
        // 임시. 게임매니저에서 `장사 시작` 할 때 호출
        SpawnCustomer(); // 시작하자마자 한명 일단 불러
        startSellingCoroutine = StartCoroutine(SpawnRoutine());
    }


    // 스폰 딜레이 시간 랜덤 설정 (fame을 가중치로)
    private float GetAdjustedSpawnDelay()
    {
        int fame;
        if (pm.GetFame() > 100) fame = 100;
        else if (pm.GetFame() < 0) fame = 0;
        else fame = pm.GetFame();

        float t = Mathf.Clamp01(fame / 100f); // 0~1로 정규화
        float minDelay = Mathf.Lerp(baseMinDelay, minCap, t);
        float maxDelay = Mathf.Lerp(baseMaxDelay, maxCap, t);
        return UnityEngine.Random.Range(minDelay, maxDelay);
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = GetAdjustedSpawnDelay();
            yield return new WaitForSeconds(delay);
            if (customers.Count < maxCustomerCount)
                SpawnCustomer(); // 손님 생성
        }
    }

    public void Update()
    {
        /*// 테스트 용
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }*/
    }

    

    // 가게로 오는 손님 생성
    public void SpawnCustomer()
    {
        // 손님 프리펩 생성
        Customer customer = Instantiate(customerPrefab, npcSpawnPoint.position, Quaternion.identity).GetComponent<Customer>(); // TODO: 스폰 위치 커스텀 하기

        // 아이스크림 메뉴 설정
        customer.SetOrder(RandomOrderStackGenerate());

        // 손님 큐에 추가
        customers.Enqueue(customer);

        // 큐 순서에 맞춰 줄 서는 위치 설정
        customer.SetTargetPoint(GetTargetPos(customer));
    }

    // 현재 손님 접대 종료
    public void RemoveFirstCustomer()
    {
        // 현재 손님 값 초기화
        NowOrder.Value = null;

        // 손님 큐에서 제거
        customers.Dequeue();

        // 줄 업데이트 (한칸씩 당기기)
        UpdateCustomerLine();
    }

    // 랜덤 아이스크림 오더 생성기 (랜덤 조합기)
    public Stack<IceCreamTasteType> RandomOrderStackGenerate()
    {
        int tasteCount = Enum.GetValues(typeof(IceCreamTasteType)).Length;
        int r = UnityEngine.Random.Range(1, maxIceCreamStackCount + 1);
        Stack<IceCreamTasteType> orderStack = new Stack<IceCreamTasteType>();

        for (int i = 0; i < r; i++)
        {
            int randomTaste = UnityEngine.Random.Range(0, tasteCount);
            orderStack.Push((IceCreamTasteType)randomTaste);
        }

        // 디버깅 용
        /*Debug.Log($"맛 개수 : {tasteCount}");
        Debug.Log(orderStack.Count);
        IceCreamTasteType[] a = orderStack.ToArray();
        foreach (IceCreamTasteType aa in a)
        {
            Debug.Log(aa);
        }*/

        return orderStack;
    }

    // 현재 상호작용 중인 손님의 주문 저장 (맨 첫번째 줄의 손님, UI반영 용도)
    public void SetNowOrder(Stack<IceCreamTasteType> order)
    {
        NowOrder.Value = order;
    }

    // 첫번째 손님이 갈 위치를 반환하는 함수. 줄의 시작점
    public Vector3 GetFirstPos()
    {
        return defaultTarget.position;
    }

    // customer가 본인의 큐 index에 맞는 줄 순서의 위치 반환하기
    public Vector3 GetTargetPos(Customer customer)
    {
        int index = Array.IndexOf(customers.ToArray(), customer);

        if (index == 0)
        {
            return defaultTarget.position;
        }
        else
        {
            return defaultTarget.position + Vector3.forward * lineDistance * index;
        }
    }

    // 현재 Customer Queue에 맞춰 모든 Customer의 TargetPoint 업데이트
    public void UpdateCustomerLine()
    {
        foreach (Customer customer in customers)
        {
            customer.SetTargetPoint(GetTargetPos(customer));
        }
    }
}
