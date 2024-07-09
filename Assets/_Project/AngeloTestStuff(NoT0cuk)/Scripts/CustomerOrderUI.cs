using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerOrderUI : MonoBehaviour
{
    [SerializeField] private OrderObject orders;
    [SerializeField] private UnityEvent UpdateOrders;

    private void Update()
    {
      if(orders.changedOrder)
      {
        orders.changedOrder = false;
        UpdateOrders.Invoke();
      }
    }
}
