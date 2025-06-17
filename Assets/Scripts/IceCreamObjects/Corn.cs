using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Corn : MonoBehaviour, IStackable
{
    // 데이터 총괄 용도 & 값 계산 용도
    Stack<IceCream> iceCreamStack = new Stack<IceCream>();

    // 주문이랑 비교 용도
    Stack<IceCreamTasteType> tasteStackData = new Stack<IceCreamTasteType>();

    public void StackIce(IceCream iceCream)
    {
        iceCreamStack.Push(iceCream);
        tasteStackData.Push(iceCream.taste);
    }
    
    public void PopIce(IceCream iceCream)
    {
        // 특정 아이스크림을 제거하면 그 위층의 아이스크림들도 전부 제거
        while (iceCreamStack.Count > 0)
        {
            IceCream top = iceCreamStack.Pop();
            tasteStackData.Pop();
            if (top == iceCream) break;
        }
    }

    public int GetPrice(int singlePrice)
    {
        return iceCreamStack.Count * singlePrice;
    }

    public Stack<IceCreamTasteType> GetTasteStackData()
    {
        return tasteStackData;
    }
}
