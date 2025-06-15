using System;
using UnityEngine.XR.Interaction.Toolkit;

public class StackableSocketInteractor : XRSocketInteractor
{
    IStackable stackable;

    protected override void Start()
    {
        base.Start();
        stackable = transform.GetComponentInParent<IStackable>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // ������ ���̽�ũ���� �� �κ� ���� Ȱ��ȭ
        IceCream uppderIceCream = args.interactableObject.transform.GetComponent<IceCream>();
        uppderIceCream.upperSocket.SetActive(true);

        if (stackable is Corn corn)
        {
            // ������ ���̽�ũ���� ���̽� �� ����
            uppderIceCream.baseCorn = corn;

            // ������ ���̽�ũ���� �� ���ÿ� �߰�
            corn.StackIce(uppderIceCream);
        }
        else if (stackable is IceCream iceCream)
        {
            // ������ ���̽�ũ���� ���̽� �� ����
            uppderIceCream.baseCorn = iceCream.baseCorn;
            
            // ������ ���̽�ũ���� �� ���ÿ� �߰�
            iceCream.baseCorn.StackIce(uppderIceCream);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // �и��� ���� �ִ� ���̽�ũ���� �� �κ� ���� ��Ȱ��ȭ
        IceCream uppderIceCream = args.interactableObject.transform.GetComponent<IceCream>();
        uppderIceCream.upperSocket.SetActive(false);
        
        if (stackable is Corn corn)
        {
            // �и��� ���̽�ũ�� ���ÿ��� ����
            corn.PopIce(uppderIceCream);
        }
        else if (stackable is IceCream iceCream)
        {
            // �и��� ���̽�ũ�� ���ÿ��� ����
            iceCream.baseCorn.PopIce(uppderIceCream);
        }
    }
}
