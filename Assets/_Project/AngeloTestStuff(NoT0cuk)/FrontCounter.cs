using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCounter : Interactable
{
    [SerializeField] private GameObjectObject frontCounter;
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private Tables tables;
    [SerializeField] private Vector2Int[] waitingSpots;
    private LinkedList<CustomerMovement> customers;
    private int currentSpot;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<CustomerMovement>();
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

    public void AddCustomer(CustomerMovement customer)
    {
      customers.AddToBack(customer);
    }

    public void SeatCustomer()
    {
      if(tablesAvailable.value != 0 && customers.Count() != 0)
      {
        customers.first.data.MoveToPosition(tables.GetChairPosition());
        customers.RemoveFirst();
        MoveAllCustomersUpOne();
      }
    }

    private void MoveAllCustomersUpOne()
    {
      currentSpot--;
      for(int a = 0; a < customers.Count(); a++)
      {
        customers.GetElementAt(a).MoveToPosition(waitingSpots[a]);
      }
    }
}
