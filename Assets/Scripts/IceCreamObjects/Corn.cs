using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Corn : MonoBehaviour, IStackable
{
    // ������ �Ѱ� �뵵 & �� ��� �뵵
    Stack<IceCream> iceCreamStack = new Stack<IceCream>();

    // �ֹ��̶� �� �뵵
    Stack<IceCreamTasteType> tasteStackData = new Stack<IceCreamTasteType>();

    public void StackIce(IceCream iceCream)
    {
        iceCreamStack.Push(iceCream);
        tasteStackData.Push(iceCream.taste);
    }
    
    public void PopIce(IceCream iceCream)
    {
        // Ư�� ���̽�ũ���� �����ϸ� �� ������ ���̽�ũ���鵵 ���� ����
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
