using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerMovement customerMovement;
    [SerializeField] private MenuObject menu;
    [SerializeField] private OrderObject orders;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private FloatObject money, stress;
    [SerializeField] private GameObject order;
    [SerializeField] private SpriteRenderer orderSprite;
    [SerializeField] private float orderWaitTime, foodWaitTime, stressIncreaseAmount, stressReductionAmount, stressUnservedPenalty;
    private int randomOrder, tableNumber;
    private string orderTag;
    private float orderPrice;
    public bool isAtReception, isAtTable, isAtCashier, hasOrdered, exiting;
    public bool paired, isPair;
    public Customer pairedCustomer;

    public CustomerMovement Movement => customerMovement;
    public int TableNumber
    {
      get => tableNumber;
      set => tableNumber = value;
    }
    public float OrderWaitTime => orderWaitTime;
    public float FoodWaitTime => foodWaitTime;
    public CustomerMovement CustomerMove => customerMovement;

    private void Start()
    {
      randomOrder = Random.Range(0, menu.orderTag.Length);
      ChooseOrder();
    }

    private void ChooseOrder()
    {
      orderTag = menu.orderTag[randomOrder];
      orderPrice = menu.orderPrice[randomOrder];
      orderSprite.sprite = menu.order[randomOrder].OrderSprite;
    }

    public void MoveCustomerToPosition(Vector2Int position)
    {
      customerMovement.MoveToPosition(position);
    }

    public void DisplayOrder()
    {
      stress.value += stressIncreaseAmount;
      hasOrdered = true;
      order.SetActive(true);
      AddOrder();
    }

    private void AddOrder()
    {
      if(isPair)
      {
        orders.pairOrder[tableNumber] = menu.order[randomOrder];
        orders.cooked[tableNumber] = false;
        orders.changedOrder = true;
      } else {
        orders.order[tableNumber] = menu.order[randomOrder];
        orders.cooked[tableNumber] = false;
        orders.changedOrder = true;
      }
    }

    public bool CheckForOrder()
    {
      if(itemHeld.value is not null)
      {
        if(itemHeld.value.tag == orderTag)
        {
          stress.value -= stressReductionAmount;
          order.SetActive(false);
          Destroy(itemHeld.value);
          itemHeld.value = null;
          if(isPair)
          {
            orders.pairOrder[tableNumber] = null;
          } else {
            orders.order[tableNumber] = null;
          }
          orders.changedOrder = true;
          customerMovement.goingToCashier = true;
          customerMovement.AnimateCustomer(6);
          Invoke("CustomerStopEating", 2.8f);
          return true;
        }
      }
      return false;
    }

    private void CustomerStopEating()
    {
      customerMovement.AnimateCustomer(4);
    }

    public void CheckOutCustomer()
    {
      exiting = true;
      money.value += orderPrice;
      customerMovement.MovePlayerToExit();
    }

    public void LeaveWithoutPaying()
    {
      if(isPair)
      {
        orders.pairOrder[tableNumber] = null;
      } else {
        orders.order[tableNumber] = null;
      }
      orders.changedOrder = true;
      order.SetActive(false);
      stress.value += stressUnservedPenalty;
      exiting = true;
      customerMovement.MovePlayerToExit();

    }
}
