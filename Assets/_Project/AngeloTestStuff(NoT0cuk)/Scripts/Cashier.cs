using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cashier : Interactable
{
    [SerializeField] private GameObjectObject cashier;
    [SerializeField] private CharacterUpgradeObject restaurantUpgrade;
    [SerializeField] private Vector2Int exit;
    [SerializeField] private Vector2Int[] waitingSpots;
    [SerializeField] private UnityEvent afterPaid;
    private LinkedList<Customer> customers;
    private int currentSpot;
    private bool checkingOut;

    public Vector2Int Exit => exit;
    public int CurrentSpot => currentSpot;
    public Vector2Int[] WaitingSpots => waitingSpots;
    public LinkedList<Customer> Customers => customers;

    public AudioSource audioSource;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<Customer>();
    }

    public override void Interact()
    {
      CheckOut();
    }

    public override void AltInteract() {}

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
        if(customers.first.data.isAtCashier)
        {
          customers.first.data.CheckOutCustomer();
          customers.RemoveFirst();
          afterPaid.Invoke();
          MoveAllCustomersUpOne();

          audioSource.Play();
          checkingOut = false;
        }
      }
    }

    private void Update()
    {
      if(restaurantUpgrade.characterUpgradeChecks[5] && !checkingOut)
      {
        if(customers.Count() != 0)
        {
          Invoke("CheckOut", 2f);
          checkingOut = true;
        }
      }
    }

    public void CheckOutAllCurrentCustomers()
    {
      if(customers.Count() != 0)
      {
        for(int a = 0; a < customers.Count() + 2; a++)
        {
          if(customers.first.data.isAtCashier)
          {
            customers.first.data.CheckOutCustomer();
            customers.RemoveFirst();
            afterPaid.Invoke();
            MoveAllCustomersUpOne();
          }
        }
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

    public void ResetCustomers()
    {
      customers = new LinkedList<Customer>();
    }
}
