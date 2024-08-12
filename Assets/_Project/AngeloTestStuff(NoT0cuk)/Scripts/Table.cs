using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : Interactable
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private GameObjectObject cashier;
    [SerializeField] private CharacterUpgradeObject restaurantUpgrade;
    [SerializeField] private int tableNumber;
    [SerializeField] private Slider tableTimer;
    [SerializeField] private Vector2Int[] chairPosition;
    private Tables tables;
    private bool servedOne, servedTwo;
    public bool tableIsFree, timerStarted, servedCustomer;
    private List<Customer> currentCustomer = new List<Customer>();
    private Cashier currentCashier;

    public Vector2Int[] ChairPosition => chairPosition;

    private void Start()
    {
      currentCashier = cashier.value.GetComponent<Cashier>();
      tables = GetComponentInParent<Tables>();
      tables.CreateTableNode(this);
    }

    private void Update()
    {
      if(timerStarted)
      {
        if(tableTimer.value > 0)
        {
          tableTimer.value -= Time.deltaTime;
        } else {
          TimeIsOut();
        }
      }
      if(servedCustomer)
      {
        ServedCustomer();
      }
    }

    private void TimeIsOut()
    {
      timerStarted = false;
      tableTimer.gameObject.SetActive(false);
      for(int a = 0; a < currentCustomer.Count; a++)
      {
        if(currentCustomer[a] is not null)
        {
          currentCustomer[a].LeaveWithoutPaying();
        }
      }
      currentCustomer = new List<Customer>();
      tableIsFree = true;
      tablesAvailable.value++;
      servedCustomer = false;
      servedOne = false;
      servedTwo = false;
    }

    public override void Interact()
    {
      if(currentCustomer.Count > 0)
      {
        if(currentCustomer[0] != null)
        {
          if(currentCustomer[0].isAtTable)
          {
            if(!currentCustomer[0].hasOrdered)
            {
              for(int a = 0; a < currentCustomer.Count; a++)
              {
                currentCustomer[a].DisplayOrder();
              }
              StartFoodTimer();
            } else {
              if(currentCustomer.Count == 1)
              {
                if(currentCustomer[0].CheckForOrder())
                {
                  tables.ServedCustomer.Invoke();
                  timerStarted = false;
                  tableTimer.gameObject.SetActive(false);
                  Invoke("ServedCustomer", 3f);
                }
              } else {
                if(!servedOne)
                {
                  if(currentCustomer[0].CheckForOrder())
                  {
                    servedOne = true;
                  }
                }
                if(!servedTwo)
                {
                  if(currentCustomer[1].CheckForOrder())
                  {
                    servedTwo = true;
                  }
                }
                if(servedOne && servedTwo)
                {
                  tables.ServedCustomer.Invoke();
                  timerStarted = false;
                  tableTimer.gameObject.SetActive(false);
                  Invoke("ServedCustomer", 3f);
                }
              }

            }
          }
        }
      }
    }

    public override void AltInteract()
    {
      if(currentCustomer.Count > 0)
      {
        if(currentCustomer[0] != null)
        {
          if(currentCustomer[0].isAtTable)
          {
            TimeIsOut();
          }
        }
      }
    }

    private void ServedCustomer()
    {
      if(currentCashier.CurrentSpot < currentCashier.WaitingSpots.Length)
      {
        for(int a = 0; a < currentCustomer.Count; a++)
        {
          currentCustomer[a].CustomerMove.MovePlayerToCashier();
        }
        currentCustomer = new List<Customer>();
        tableIsFree = true;
        tablesAvailable.value++;
        servedCustomer = false;
        servedOne = false;
        servedTwo = false;
      } else {
        servedCustomer = true;
      }
    }

    public void AddCustomerToTable(Customer customer)
    {
      currentCustomer.Add(customer);
      customer.TableNumber = tableNumber;
      StartOrderTimer();
    }

    private void StartOrderTimer()
    {
      tableTimer.gameObject.SetActive(true);

      if(restaurantUpgrade.characterUpgradeChecks[3])
      {
        tableTimer.maxValue = currentCustomer[0].FoodWaitTime + 20;
      } else {
        tableTimer.maxValue = currentCustomer[0].FoodWaitTime;
      }
      tableTimer.value = tableTimer.maxValue;
      timerStarted = true;
    }

    private void StartFoodTimer()
    {
      timerStarted = false;
      if(restaurantUpgrade.characterUpgradeChecks[3])
      {
        tableTimer.maxValue = currentCustomer[0].FoodWaitTime + 40;
      } else {
        tableTimer.maxValue = currentCustomer[0].FoodWaitTime;
      }
      tableTimer.value = tableTimer.maxValue;
      timerStarted = true;
    }

    public void ResetTable()
    {
      timerStarted = false;
      tableTimer.gameObject.SetActive(false);
      currentCustomer = new List<Customer>();
      tableIsFree = true;
      servedCustomer = false;
    }
}
