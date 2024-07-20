using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCounter : Interactable
{
    [SerializeField] private GameObjectObject frontCounter;
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private Tables tables;
    [SerializeField] private  Vector2Int[] waitingSpots;
    private LinkedList<Customer> customers;
    private int currentSpot;

    public Vector2Int[] WaitingSpots => waitingSpots;
    public LinkedList<Customer> Customers => customers;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<Customer>();
    }

    public override void Interact()
    {
      SeatCustomer();
    }

    private void SetFrontCounter()
    {
      frontCounter.value = gameObject;
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

    private void SeatCustomer()
    {
      if(tablesAvailable.value != 0 && customers.Count() != 0)
      {
        customers.first.data.MoveCustomerToPosition(tables.GetChairPosition(customers.first.data));
        customers.first.data.Movement.goingToTable = true;
        customers.RemoveFirst();
        MoveAllCustomersUpOne();
      }
    }

    private void MoveAllCustomersUpOne()
    {
      currentSpot--;
      for(int a = 0; a < waitingSpots.Length; a++)
      {
        if(a < customers.Count())
        {
          customers.GetElementAt(a).MoveCustomerToPosition(waitingSpots[a]);
        }
      }
    }
}
