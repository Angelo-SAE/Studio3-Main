using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{
    [SerializeField] private OrderObject orders;
    [SerializeField] private int orderNumber;
    [SerializeField] private Image slotSprite;
    [SerializeField] private bool isCustomerTwo;

    public void OnNotifty()
    {
      UpdateOrderSlot();
    }

    private void UpdateOrderSlot()
    {
      if(isCustomerTwo)
      {
        if(orders.pairOrder[orderNumber] is not null)
        {
          slotSprite.color = new Color(1, 1, 1, 1);
          slotSprite.sprite = orders.pairOrder[orderNumber].OrderSprite;
        } else {
          slotSprite.sprite = null;
          slotSprite.color = new Color(1,1,1,0);
        }
      } else {
        if(orders.order[orderNumber] is not null)
        {
          slotSprite.color = new Color(1, 1, 1, 1);
          slotSprite.sprite = orders.order[orderNumber].OrderSprite;
        } else {
          slotSprite.sprite = null;
          slotSprite.color = new Color(1,1,1,0);
        }
      }

    }
}
