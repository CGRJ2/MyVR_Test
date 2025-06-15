using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    [SerializeField] int maxIceCreamLayer;
    [SerializeField] GameObject customerPrefab;

    public void Init()
    {
        base.SingletonInit();
    }

    public void Start()
    {
        RandomOrderStackGenerate();
    }

    public void SpawnCustomer()
    {
        Instantiate(customerPrefab);
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

    // ���� �� ���¸� UI�� �ݿ� (���̽�ũ�� �ֹ� ����, ���� �ð�, ǥ��)
    public void ApplyUi()
    {

    }
}
