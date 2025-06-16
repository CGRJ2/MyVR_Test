using DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    [SerializeField] int maxIceCreamLayer;
    [SerializeField] int maxCustomerCounts;
    [SerializeField] float lineDistance;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform defaultTarget;
    [SerializeField] Transform npcSpawnPoint;
    Queue<Customer> customers = new Queue<Customer>();

    /*[SerializeField]*/ public Customer nowCustomer;

    public void Init()
    {
        base.SingletonInit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (customers.Count < maxCustomerCounts)
            {
                SpawnCustomer();
            }
        }
    }

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

    public void RemoveFirstCustomer()
    {
        // 손님 큐에서 제거
        customers.Dequeue();

        // 줄 업데이트 (한칸씩 당기기)
        UpdateCustomerLine();
    }

    public Stack<IceCreamTasteType> RandomOrderStackGenerate()
    {
        int tasteCount = Enum.GetValues(typeof(IceCreamTasteType)).Length;
        int r = UnityEngine.Random.Range(1, maxIceCreamLayer + 1);
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

    public void UpdateCustomerLine()
    {
        foreach(Customer customer in customers)
        {
            customer.SetTargetPoint(GetTargetPos(customer));
            customer.agent.SetDestination(GetTargetPos(customer));
        }
    }

    // 현재 상호작용 중인 손님 (맨 첫번째 줄의 손님)
    public void SetNowCustomer(Customer customer)
    {
        nowCustomer = customer;

        // UI 업데이트, 활성화
    }

    public Vector3 GetFirstPos()
    {
        return defaultTarget.position;
    }
}
