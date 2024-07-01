using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField] private IntObject tablesAvailable;
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
        if(!currentCustomer.HasOrdered)
        {
          currentCustomer.DisplayOrder();
        } else {
          if(currentCustomer.CheckForOrder())
          {
            currentCustomer = null;
            tableIsFree = true;
            tablesAvailable.value++;
            tables.ServedCustomer.Invoke();
          }
        }
      }
    }

    public void AddCustomerToTable(Customer customer)
    {
      currentCustomer = customer;
    }
}
