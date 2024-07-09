using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{
    [SerializeField] private OrderObject orders;
    [SerializeField] private int orderNumber;
    [SerializeField] private Image slotSprite;

    public void OnNotifty()
    {
      UpdateOrderSlot();
    }

    private void UpdateOrderSlot()
    {
      if(orders.order[orderNumber] is not null)
      {
        slotSprite.sprite = orders.order[orderNumber].OrderSprite;
      } else {
        slotSprite.sprite = null;
      }
    }
}
