using DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    // �մ� ���� ���� ��ƾ

    PlayerManager pm;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform defaultTarget;
    [SerializeField] Transform npcSpawnPoint;
    [SerializeField] int maxIceCreamStackCount;
    [SerializeField] int maxCustomerCount;
    [SerializeField] float lineDistance;
    [SerializeField] float baseMinDelay;
    [SerializeField] float baseMaxDelay;
    [SerializeField] float minCap; // ��ġ �ִ� �� �� min
    [SerializeField] float maxCap; // ��ġ �ִ� �� �� max

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
        // �ӽ�. ���ӸŴ������� `��� ����` �� �� ȣ��
        SpawnCustomer(); // �������ڸ��� �Ѹ� �ϴ� �ҷ�
        startSellingCoroutine = StartCoroutine(SpawnRoutine());
    }


    // ���� ������ �ð� ���� ���� (fame�� ����ġ��)
    private float GetAdjustedSpawnDelay()
    {
        int fame;
        if (pm.GetFame() > 100) fame = 100;
        else if (pm.GetFame() < 0) fame = 0;
        else fame = pm.GetFame();

        float t = Mathf.Clamp01(fame / 100f); // 0~1�� ����ȭ
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
                SpawnCustomer(); // �մ� ����
        }
    }

    public void Update()
    {
        /*// �׽�Ʈ ��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }*/
    }

    

    // ���Է� ���� �մ� ����
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

    // ���� �մ� ���� ����
    public void RemoveFirstCustomer()
    {
        // ���� �մ� �� �ʱ�ȭ
        NowOrder.Value = null;

        // �մ� ť���� ����
        customers.Dequeue();

        // �� ������Ʈ (��ĭ�� ����)
        UpdateCustomerLine();
    }

    // ���� ���̽�ũ�� ���� ������ (���� ���ձ�)
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

    // ���� ��ȣ�ۿ� ���� �մ��� �ֹ� ���� (�� ù��° ���� �մ�, UI�ݿ� �뵵)
    public void SetNowOrder(Stack<IceCreamTasteType> order)
    {
        NowOrder.Value = order;
    }

    // ù��° �մ��� �� ��ġ�� ��ȯ�ϴ� �Լ�. ���� ������
    public Vector3 GetFirstPos()
    {
        return defaultTarget.position;
    }

    // customer�� ������ ť index�� �´� �� ������ ��ġ ��ȯ�ϱ�
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

    // ���� Customer Queue�� ���� ��� Customer�� TargetPoint ������Ʈ
    public void UpdateCustomerLine()
    {
        foreach (Customer customer in customers)
        {
            customer.SetTargetPoint(GetTargetPos(customer));
        }
    }
}
