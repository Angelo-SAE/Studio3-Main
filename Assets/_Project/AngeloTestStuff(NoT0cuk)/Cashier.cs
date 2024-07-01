using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cashier : Interactable
{
    [SerializeField] private GameObjectObject cashier;
    [SerializeField] private Vector2Int[] waitingSpots;
    [SerializeField] private UnityEvent afterPaid;
    private LinkedList<Customer> customers;
    private int currentSpot;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<Customer>();
    }

    public override void Interact()
    {
      CheckOut();
    }

    private void SetFrontCounter()
    {
      cashier.value = gameObject;
    }

    public Vector2Int GetNextSpot()
    {
      Vector2Int temp = waitingSpots[currentSpot];
      currentSpot++;
      return temp;
    }

    public void AddCustomer(Customer customer)
    {
      customers.AddToBack(customer);
    }

    private void CheckOut()
    {
      if(customers.Count() != 0)
      {
        customers.first.data.CheckOutCustomer();
        customers.RemoveFirst();
        afterPaid.Invoke();
        MoveAllCustomersUpOne();
      }
    }

    private void MoveAllCustomersUpOne()
    {
      currentSpot--;
      for(int a = 0; a < customers.Count(); a++)
      {
        customers.GetElementAt(a).MoveCustomerToPosition(waitingSpots[a]);
      }
    }
}
