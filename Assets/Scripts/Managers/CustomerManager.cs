using DesignPattern;
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
    Stack<Customer> customers = new Stack<Customer>();

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

        // 줄 서는 위치 설정
        customer.SetTargetPoint(GetTargetPos());

        // 손님 줄에 추가
        customers.Push(customer);
    }

    public Stack<IceCreamTasteType> RandomOrderStackGenerate()
    {
        int tasteCount = System.Enum.GetValues(typeof(IceCreamTasteType)).Length;
        int r = Random.Range(1, maxIceCreamLayer + 1);
        Stack<IceCreamTasteType> orderStack = new Stack<IceCreamTasteType>();

        for (int i = 0; i < r; i++)
        {
            int randomTaste = Random.Range(0, tasteCount);
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

    public Vector3 GetTargetPos()
    {
        if (customers.Count <= 0)
        {
            return defaultTarget.position;
        }
        else
        {
            return defaultTarget.position + Vector3.forward * lineDistance * customers.Count;
        }
    }

    
}
