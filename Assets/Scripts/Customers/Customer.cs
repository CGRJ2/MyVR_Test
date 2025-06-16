using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    CustomerManager cm;

    public Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;

    Vector3 leavePoint;
    Vector3 targetPoint;
    public NavMeshAgent agent;
    bool activateOrder = false;

    private void Start()
    {
        // �ӽ� ///////////////////////
        leavePoint = new Vector3(8, 0, 8);
        ///////////////////////////////


        cm = CustomerManager.Instance;

        // ���� ��, ���̽�ũ�� �Ǹ� ��� �̵�
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint);

        // �����ϸ� UI �ݿ�(UIManager ȣ��)
    }

    private void Update()
    {
        if (ReachedDestination(agent))
        {
            // ù��° �ٿ� ���� && �ֹ� ���� �� ���¶��
            if (agent.destination.x == cm.GetFirstPos().x
                && agent.destination.z == cm.GetFirstPos().z
                && !activateOrder)
            {
                ArriveFirst();
            }
        }

        // �׽�Ʈ �뵵
        if (Input.GetKeyDown(KeyCode.G))
        {
            IceCreamTasteType[] a = orderStack.ToArray();
            foreach (IceCreamTasteType aa in a)
            {
                Debug.Log(aa);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (orderStack == cm.NowOrder.Value)
            Leave();
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
    }

    bool ReachedDestination(NavMeshAgent agent)
    {
        return !agent.pathPending                          // ���� ��� ��� �� �ƴ�
            && agent.remainingDistance <= agent.stoppingDistance  // ���� �� �԰�
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f); // �����
    }

    public void ArriveFirst()
    {
        activateOrder = true;
        cm.SetNowOrder(orderStack);
        UIManager.Instance.ActivateOrderPanel();
    }

    // ù��° �մ� ������
    // ���� or �Ҹ��� üũ
    public void Leave()
    {
        agent.SetDestination(leavePoint);
        cm.RemoveFirstCustomer();
        UIManager.Instance.DeactivateOrderPanel();
    }


    // �մ� Ÿ�� (��ȭ, ����, �ɼ�)

    // ���� ���� ���� �Լ� & ���� ����
    // �մ� ���ݿ� ���� �ð� �������ִ� �Լ�
    // ���� �ð� ��� ���� �ð� ������ ���� ǥ�� ��ȭ (�ܰ躰 ��ȭ & �ð� UI�� �ݿ�)

    // TODO: �մ� ���� ��, �� ���� ������Ʈ (�� ĭ�� ������ �̵� & ���� 1�� �մ� ������Ʈ)



    private void OnTriggerEnter(Collider other)
    {
        // �̰͵� ������ ���Ǿ�� �ٲ���ҵ�.
        Debug.Log("���̽�ũ�� ����");
    }
}
