using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherOrder : MonoBehaviour
{
    [SerializeField] private OrderObject orders;
    [SerializeField] private MenuObject menu;
    [SerializeField] private int orderNumber, tableNumber;

    public void PlaceOrder()
    {
      orders.order[tableNumber] = menu.order[orderNumber];
      orders.cooked[tableNumber] = false;
      orders.changedOrder = true;
    }
}
