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

        // 1. ���� ���� ���
        interactionManager.SelectExit(interactor, this);

        // 2. ������ ����
        GameObject newObject = Instantiate(cornPrefab, transform.position, transform.rotation);
        XRGrabInteractable newGrab = newObject.GetComponent<XRGrabInteractable>();

        // 3. �������� select �õ�
        interactionManager.SelectEnter(interactor, newGrab);
    }
}
