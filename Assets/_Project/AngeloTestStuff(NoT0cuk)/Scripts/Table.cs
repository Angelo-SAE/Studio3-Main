using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : Interactable
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private GameObjectObject cashier;
    [SerializeField] private int tableNumber;
    [SerializeField] private Slider tableTimer;
    public Vector2Int chairPosition;
    private Tables tables;
    public bool tableIsFree, timerStarted, servedCustomer;
    private Customer currentCustomer;
    private Cashier currentCashier;

    private void Awake()
    {
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
          timerStarted = false;
          tableTimer.gameObject.SetActive(false);
          currentCustomer.LeaveWithoutPaying();
          ServedCustomer();
        }
      }
      if(servedCustomer)
      {
        ServedCustomer();
      }
    }

    public override void Interact()
    {
      if(currentCustomer != null)
      {
        if(currentCustomer.isAtTable)
        {
          if(!currentCustomer.hasOrdered)
          {
            currentCustomer.DisplayOrder();
            StartFoodTimer();
          } else {
            if(currentCustomer.CheckForOrder())
            {
              tables.ServedCustomer.Invoke();
              timerStarted = false;
              tableTimer.gameObject.SetActive(false);
              currentCashier = cashier.value.GetComponent<Cashier>();
              Invoke("ServedCustomer", 3f);
            }
          }
        }
      }
    }

    private void ServedCustomer()
    {
      if(currentCashier.CurrentSpot < currentCashier.WaitingSpots.Length)
      {
        currentCustomer.CustomerMove.MovePlayerToCashier();
        currentCustomer = null;
        tableIsFree = true;
        tablesAvailable.value++;
        servedCustomer = false;
      } else {
        servedCustomer = true;
      }
    }

    public void AddCustomerToTable(Customer customer)
    {
      currentCustomer = customer;
      currentCustomer.TableNumber = tableNumber;
      StartOrderTimer();
    }

    private void StartOrderTimer()
    {
      tableTimer.gameObject.SetActive(true);
      tableTimer.maxValue = currentCustomer.OrderWaitTime;
      tableTimer.value = tableTimer.maxValue;
      timerStarted = true;
    }

    private void StartFoodTimer()
    {
      timerStarted = false;
      tableTimer.maxValue = currentCustomer.FoodWaitTime;
      tableTimer.value = tableTimer.maxValue;
      timerStarted = true;
    }
}
