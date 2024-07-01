using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerMovement customerMovement;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private FloatObject money;
    [SerializeField] private GameObject order;
    [SerializeField] private string orderTag;
    [SerializeField] private float orderValue;
    private bool hasOrdered;

    public bool HasOrdered => hasOrdered;

    public void MoveCustomerToPosition(Vector2Int position)
    {
      customerMovement.MoveToPosition(position);
    }

    public void DisplayOrder()
    {
      hasOrdered = true;
      order.SetActive(true);
    }

    public bool CheckForOrder()
    {
      if(itemHeld.value is not null)
      {
        if(itemHeld.value.tag == orderTag)
        {
          order.SetActive(false);
          Destroy(itemHeld.value);
          itemHeld.value = null;
          customerMovement.MovePlayerToCashier();
          return true;
        }
      }
      return false;
    }

    public void CheckOutCustomer()
    {
      money.value += orderValue;
      Destroy(gameObject);
    }
}
