using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerManager cm;

    Stack<IceCreamTasteType> orderStack = new Stack<IceCreamTasteType>();
    float timeLimit;
    float currentTime;


    private void Start()
    {
        cm = CustomerManager.Instance;

        // ���� ��, ���̽�ũ�� �Ǹ� ��� �̵�

    }

    public void InitCustomer()
    {
        SetCustomerDatas();
        //UI �ݿ�
    }

    public void SetCustomerDatas()
    {
        orderStack = cm.RandomOrderStackGenerate();

        // �ð����ѵ� Ÿ�Կ� ���� ���� �������� �߰�
    }
    
    // �մ� Ÿ�� (��ȭ, ����, �ɼ�)

    // ���� ���� ���� �Լ� & ���� ����
    // �մ� ���ݿ� ���� �ð� �������ִ� �Լ�
    // ���� �ð� ��� ���� �ð� ������ ���� ǥ�� ��ȭ (�ܰ躰 ��ȭ & �ð� UI�� �ݿ�)
}
