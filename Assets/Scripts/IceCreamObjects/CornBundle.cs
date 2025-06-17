using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CornBundle : XRBaseInteractable
{
    [SerializeField] GameObject cornPrefab;

    public void InstantiateCorn(SelectEnterEventArgs args)
    {
        Instantiate(cornPrefab);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        var interactionManager = this.interactionManager;

        // 1. 본인 선택 취소
        interactionManager.SelectExit(interactor, this);

        // 2. 프리팹 생성
        GameObject newObject = Instantiate(cornPrefab, transform.position, transform.rotation);
        XRGrabInteractable newGrab = newObject.GetComponent<XRGrabInteractable>();

        // 3. 프리펩을 select 시도
        interactionManager.SelectEnter(interactor, newGrab);
    }
}
