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

    // 현재 고객 상태를 UI에 반영 (아이스크림 주문 정보, 남은 시간, 표정)
    public void ApplyUi()
    {

    }
}
