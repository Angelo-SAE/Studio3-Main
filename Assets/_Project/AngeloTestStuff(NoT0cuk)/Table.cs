using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField] private Vector2Int chairPosition;
    private Tables tables;
    private Customer currentCustomer;

    private void Awake()
    {
      tables = GetComponentInParent<Tables>();
      tables.CreateTableNode(this, chairPosition);
    }

    public override void Interact()
    {
      if(currentCustomer != null && !currentCustomer.HasOrdered)
      {
        currentCustomer.DisplayOrder();
      }
    }

    public void AddCustomerToTable(Customer customer)
    {
      currentCustomer = customer;
    }
}
