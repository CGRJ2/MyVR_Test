using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;
    

    Vector3 targetPoint;
    NavMeshAgent agent;


    private void Start()
    {
        // ���� ��, ���̽�ũ�� �Ǹ� ��� �̵�
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint);

        // �����ϸ� UI �ݿ�(UIManager ȣ��)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IceCreamTasteType[] a = orderStack.ToArray();
            foreach (IceCreamTasteType aa in a)
            {
                Debug.Log(aa);
            }
        }
    }

    public void InitCustomer()
    {

    }

    public void SetOrder(Stack<IceCreamTasteType> order)
    {
        orderStack = order;
    }
    
    public void SetTargetPoint(Vector3 target)
    {
        targetPoint = target;
        // �ӽ�
    }

    // �մ� Ÿ�� (��ȭ, ����, �ɼ�)

    // ���� ���� ���� �Լ� & ���� ����
    // �մ� ���ݿ� ���� �ð� �������ִ� �Լ�
    // ���� �ð� ��� ���� �ð� ������ ���� ǥ�� ��ȭ (�ܰ躰 ��ȭ & �ð� UI�� �ݿ�)

    // TODO: �մ� ���� ��, �� ���� ������Ʈ (�� ĭ�� ������ �̵� & ���� 1�� �մ� ������Ʈ)
}
