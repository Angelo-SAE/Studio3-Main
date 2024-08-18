using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontCounter : Interactable
{
    [SerializeField] private GameObjectObject frontCounter;
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private Tables tables;
    [SerializeField] private GameObject counterTimer;
    [SerializeField] private Slider counterTime;
    [SerializeField] private float timerSpeed;
    [SerializeField] private Vector2Int[] waitingSpots, pairWaitingSpots;
    private LinkedList<Customer> customers;
    private int currentSpot;
    private bool timerRunning;
    private float currentTimerTime;

    public Vector2Int[] WaitingSpots => waitingSpots;
    public Vector2Int[] PairWaitingSpots => pairWaitingSpots;
    public LinkedList<Customer> Customers => customers;
    public int CurrentSpot => currentSpot;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<Customer>();
    }

    public override void Interact()
    {
      SeatCustomer();
    }

    public override void AltInteract() {}

    private void Update()
    {
      if(timerRunning)
      {
        currentTimerTime -= Time.deltaTime * timerSpeed;
        if(currentTimerTime <= 0)
        {
          KickOutCustomer();
          CheckForTimer();
        }
        counterTime.value = currentTimerTime;
      }
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
      if(customers.Count() == 1)
      {
        CheckForTimer();
      }
    }

    private void SeatCustomer()
    {
      if(tablesAvailable.value != 0 && customers.Count() != 0)
      {
        if(customers.first.data.paired)
        {
          SeatTwoCustomers();
        } else {
          SeatOneCustomer();
          MoveAllCustomersUpOne();
        }
      }
    }

    private void SeatOneCustomer()
    {
      customers.first.data.MoveCustomerToPosition(tables.GetChairPosition(customers.first.data));
      customers.first.data.Movement.goingToTable = true;
      customers.first.data.Movement.goingToReception = false;
      customers.RemoveFirst();
    }

    private void SeatTwoCustomers()
    {
      Customer[] tempCustomers = new Customer[] {customers.first.data, customers.first.data.pairedCustomer};
      Vector2Int[] tempPositions = tables.GetChairPositions(tempCustomers);
      customers.first.data.Movement.goingToTable = true;
      customers.first.data.Movement.goingToReception = false;
      customers.first.data.MoveCustomerToPosition(tempPositions[0]);
      customers.first.data.pairedCustomer.Movement.goingToTable = true;
      customers.first.data.pairedCustomer.Movement.goingToReception = false;
      customers.first.data.pairedCustomer.MoveCustomerToPosition(tempPositions[1]);
      customers.RemoveFirst();
      MoveAllCustomersUpOne();
    }

    private void MoveAllCustomersUpOne()
    {
      currentSpot--;
      for(int a = 0; a < waitingSpots.Length; a++)
      {
        if(a < customers.Count())
        {
          Customer tempCustomer = customers.GetElementAt(a);
          tempCustomer.MoveCustomerToPosition(waitingSpots[a]);
          if(tempCustomer.paired)
          {
            tempCustomer.pairedCustomer.MoveCustomerToPosition(pairWaitingSpots[a]);
          }
        }
      }
      CheckForTimer();
    }

    private void CheckForTimer()
    {
      if(customers.Count() > 0)
      {
        counterTimer.SetActive(true);
        counterTime.value = 10;
        currentTimerTime = 10;
        timerRunning = true;
      } else {
        timerRunning = false;
        counterTimer.SetActive(false);
      }
    }

    private void KickOutCustomer()
    {
      if(customers.first.data.paired)
      {
        customers.first.data.LeaveWithoutPaying();
        customers.first.data.pairedCustomer.LeaveWithoutPaying();
      } else {
        customers.first.data.LeaveWithoutPaying();
      }
      customers.RemoveFirst();
      MoveAllCustomersUpOne();
    }

    public void ResetCustomers()
    {
      customers = new LinkedList<Customer>();
      currentSpot = 0;
      CheckForTimer();
    }
}
