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
        // �մ� ������ ����
        Customer customer = Instantiate(customerPrefab, npcSpawnPoint.position, Quaternion.identity).GetComponent<Customer>(); // TODO: ���� ��ġ Ŀ���� �ϱ�

        // ���̽�ũ�� �޴� ����
        customer.SetOrder(RandomOrderStackGenerate());

        // �մ� ť�� �߰�
        customers.Enqueue(customer);

        // ť ������ ���� �� ���� ��ġ ����
        customer.SetTargetPoint(GetTargetPos(customer));
    }

    public void RemoveFirstCustomer()
    {
        // �մ� ť���� ����
        customers.Dequeue();

        // �� ������Ʈ (��ĭ�� ����)
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

        // ����� ��
        /*Debug.Log($"�� ���� : {tasteCount}");
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

    // ���� ��ȣ�ۿ� ���� �մ� (�� ù��° ���� �մ�)
    public void SetNowCustomer(Customer customer)
    {
        nowCustomer = customer;

        // UI ������Ʈ, Ȱ��ȭ
    }

    public Vector3 GetFirstPos()
    {
        return defaultTarget.position;
    }
}
