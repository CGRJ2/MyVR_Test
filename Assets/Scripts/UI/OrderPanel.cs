using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] List<Image> iceCreamImages = new List<Image>();
    Color strawberry = Color.HSVToRGB(0f, 0.42f, 1f);
    Color chocolate = Color.HSVToRGB(32f / 360f, 1f, 0.65f);
    Color vanilla = Color.HSVToRGB(51f / 360f, 0.57f, 1f);

    private void Start()
    {
        CustomerManager.Instance.NowOrder.Subscribe(SetImagesByOrder);
    }

    public void SetImagesByOrder(Stack<IceCreamTasteType> orderStack)
    {
        if (orderStack == null)
        {
            // 이미지 초기화
            foreach (Image image in iceCreamImages)
            {
                image.gameObject.SetActive(false);
            }
        }
        else
        {
            IceCreamTasteType[] order = orderStack.ToArray();

            for (int i = 0; i < order.Length; i++)
            {
                switch (order[i])
                {
                    case IceCreamTasteType.Strawberry:
                        iceCreamImages[i].color = strawberry;
                        break;
                    case IceCreamTasteType.Chocolate:
                        iceCreamImages[i].color = chocolate;
                        break;
                    case IceCreamTasteType.Vanilla:
                        iceCreamImages[i].color = vanilla;
                        break;
                }
                iceCreamImages[i].gameObject.SetActive(true);
            }
        }
    }
}
