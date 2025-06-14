using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IceSocketController : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // 소켓에 붙으면 아이스크림인지 판단 먼저
        // 위에 붙은 놈이니까. 붙은 놈에다가 uppersocket 활성화해줌
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
