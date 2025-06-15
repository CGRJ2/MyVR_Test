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

        // 부착된 아이스크림의 윗 부분 소켓 활성화
        IceCream uppderIceCream = args.interactableObject.transform.GetComponent<IceCream>();
        uppderIceCream.upperSocket.SetActive(true);

        if (stackable is Corn corn)
        {
            // 부착된 아이스크림에 베이스 콘 설정
            uppderIceCream.baseCorn = corn;

            // 부착된 아이스크림을 콘 스택에 추가
            corn.StackIce(uppderIceCream);
        }
        else if (stackable is IceCream iceCream)
        {
            // 부착된 아이스크림에 베이스 콘 설정
            uppderIceCream.baseCorn = iceCream.baseCorn;
            
            // 부착된 아이스크림을 콘 스택에 추가
            iceCream.baseCorn.StackIce(uppderIceCream);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // 분리된 위에 있는 아이스크림은 윗 부분 소켓 비활성화
        IceCream uppderIceCream = args.interactableObject.transform.GetComponent<IceCream>();
        uppderIceCream.upperSocket.SetActive(false);
        
        if (stackable is Corn corn)
        {
            // 분리된 아이스크림 스택에서 제거
            corn.PopIce(uppderIceCream);
        }
        else if (stackable is IceCream iceCream)
        {
            // 분리된 아이스크림 스택에서 제거
            iceCream.baseCorn.PopIce(uppderIceCream);
        }
    }
}
