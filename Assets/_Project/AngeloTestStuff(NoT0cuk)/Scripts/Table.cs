using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private int tableNumber;
    public Vector2Int chairPosition;
    private Tables tables;
    public bool tableIsFree;
    private Customer currentCustomer;

    private void Awake()
    {
      tables = GetComponentInParent<Tables>();
      tables.CreateTableNode(this);
    }

    public override void Interact()
    {
      if(currentCustomer != null)
      {
        if(currentCustomer.isAtTable)
        {
          if(!currentCustomer.hasOrdered)
          {
            currentCustomer.DisplayOrder(tableNumber);
          } else {
            if(currentCustomer.CheckForOrder())
            {
              tables.ServedCustomer.Invoke();
              Invoke("ServedCustomer", 3f);
            }
          }
        }
      }
    }

    private void ServedCustomer()
    {
      currentCustomer = null;
      tableIsFree = true;
      tablesAvailable.value++;
    }

    public void AddCustomerToTable(Customer customer)
    {
      currentCustomer = customer;
    }
}
