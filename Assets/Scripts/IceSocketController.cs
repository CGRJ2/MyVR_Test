using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IceSocketController : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // ���Ͽ� ������ ���̽�ũ������ �Ǵ� ����
        // ���� ���� ���̴ϱ�. ���� �𿡴ٰ� uppersocket Ȱ��ȭ����
        IXRSelectInteractable attachedObj = args.interactableObject;
        attachedObj.transform.GetComponent<IceCream>().upperSocket.SetActive(true);

        //Debug.Log(args.interactableObject.transform.gameObject.name);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        IXRSelectInteractable attachedObj = args.interactableObject;
        attachedObj.transform.GetComponent<IceCream>().upperSocket.SetActive(false);
    }
}
