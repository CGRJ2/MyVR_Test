using DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Customer : MonoBehaviour
{
    [SerializeField] int singleIcePrice;
    [SerializeField] float maxTimer;
    [SerializeField] float minTimer;
    Coroutine timerCorountine;

    CustomerManager cm;
    public Stack<IceCreamTasteType> orderStack;
    float timeLimit;
    float currentTime;

    Vector3 leavePoint;
    public ObservableProperty<Vector3> TargetPoint = new();

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
        agent.SetDestination(TargetPoint.Value);
    }

    private void OnEnable()
    {
        TargetPoint.Subscribe(MoveToTarget);
    }
    private void OnDisable()
    {
        TargetPoint.UnsbscribeAll();
        StopCoroutine(timerCorountine);
    }

    // Ÿ���� ������Ʈ �Ǹ� �ڵ����� �̵� ���� (TargetPoint�� �̺�Ʈ ����)
    void MoveToTarget(Vector3 target)
    {
        if (agent != null)
            agent.SetDestination(target);
    }

    private void Update()
    {
        // �������� ������ ����
        if (ReachedDestination(agent))
        {
            // �������� ù��° ���̶�� && �ֹ� ���� �� ���¶��
            if (agent.destination.x == cm.GetFirstPos().x
                && agent.destination.z == cm.GetFirstPos().z
                && !activateOrder)
            {
                ArriveFirst();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (orderStack == cm.NowOrder.Value)
            Leave();
        }
    }

    // �մ� ���� ��, ������� �ֹ� ������ ���� (cm���� ȣ��)
    public void SetOrder(Stack<IceCreamTasteType> order)
    {
        orderStack = order;
    }

    // navy mesh agent Ÿ�� ���� �뵵
    public void SetTargetPoint(Vector3 target)
    {
        TargetPoint.Value = target;
    }

    // navy mesh agent ������ ���� �Ǻ�
    bool ReachedDestination(NavMeshAgent agent)
    {
        return !agent.pathPending                          // ���� ��� ��� �� �ƴ�
            && agent.remainingDistance <= agent.stoppingDistance  // ���� �� �԰�
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f); // �����
    }

    // ù��° �մ��� �Ǿ��� �� �� ���� ȣ��
    public void ArriveFirst()
    {
        activateOrder = true; // �� ���� ȣ���ϱ� ���� �÷���
        cm.SetNowOrder(orderStack);
        UIManager.Instance.ActivateOrderPanel();
        timerCorountine = StartCoroutine(TimerCoroutine());
    }

    public void SetRandomTimer()
    {
        timeLimit = UnityEngine.Random.Range(minTimer, maxTimer);
    }
    private IEnumerator TimerCoroutine()
    {
        SetRandomTimer();
        currentTime = 0;
        Debug.Log("Ÿ�̸� ����");
        while (currentTime < timeLimit)
        {
            UIManager.Instance.SetTimerUI(timeLimit - currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Ÿ�̸� �Ϸ�");

        // Ÿ�� �ƿ� �г� <<<
        UIManager.Instance.SetTimerUI("Where is my ice cream?");
        yield return new WaitForSeconds(1f);
        TimeOut();
    }

    // ù��° �մ� ������
    // ���� or �Ҹ��� üũ
    public void Leave()
    {
        agent.SetDestination(leavePoint);
        cm.RemoveFirstCustomer();
        UIManager.Instance.DeactivateOrderPanel();
    }

    public void ReceiveIceCream(SelectEnterEventArgs args)
    {
        Corn corn = args.interactableObject.transform.GetComponent<Corn>();
        PlayerManager pm = PlayerManager.Instance;

        if (corn.GetTasteStackData().SequenceEqual(orderStack))
        {
            // ���� �� ����
            pm.GetMoney(corn.GetPrice(singleIcePrice));
            pm.GetFame(5);

            // �����ϴ� �ִϸ��̼� ����
        }
        else
        {
            // �ֹ��� �ٸ� ���̽�ũ�� �� ��ŭ �� ����
            IceCreamTasteType[] orderArray = orderStack.ToArray();
            IceCreamTasteType[] myArray = corn.GetTasteStackData().ToArray();
            int wrongIceCount = 0;
            for (int i = 0; i < orderArray.Length; i++)
            {
                if (myArray.Length <= i)
                {
                    wrongIceCount += 1;
                    continue;
                }

                if (orderArray[i] != myArray[i]) wrongIceCount += 1;
            }

            // �� ���ݸ� ����, �� ����
            pm.GetMoney(corn.GetPrice(singleIcePrice)/2);
            pm.GetFame(-wrongIceCount);


            // �Ҹ����ϴ� �ִϸ��̼� ����
        }

        // �ִϸ��̼� ���� ��
        Leave();
    }


    public void TimeOut()
    {
        PlayerManager pm = PlayerManager.Instance;
        pm.GetFame(-5);
        Leave();
    }


    // �մ� Ÿ�� (��ȭ, ����, �ɼ�)

    // ���� ���� ���� �Լ� & ���� ����
    // �մ� ���ݿ� ���� �ð� �������ִ� �Լ�
    // ���� �ð� ��� ���� �ð� ������ ���� ǥ�� ��ȭ (�ܰ躰 ��ȭ & �ð� UI�� �ݿ�)

    // TODO: �մ� ������ ���� ����

    

    

    private void OnTriggerEnter(Collider other)
    {
        // �̰͵� ������ ���Ǿ�� �ٲ���ҵ�.
        Debug.Log("���̽�ũ�� ����");
    }
}
