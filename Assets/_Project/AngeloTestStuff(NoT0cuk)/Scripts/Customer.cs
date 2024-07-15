using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerMovement customerMovement;
    [SerializeField] private MenuObject menu;
    [SerializeField] private OrderObject orders;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private FloatObject money;
    [SerializeField] private GameObject order;
    [SerializeField] private SpriteRenderer orderSprite;
    [SerializeField] private float orderWaitTime, foodWaitTime;
    private int randomOrder, tableNumber;
    private string orderTag;
    private float orderPrice;
    public bool isAtReception, isAtTable, isAtCashier, hasOrdered, exiting;

    public CustomerMovement Movement => customerMovement;
    public int TableNumber
    {
      get => tableNumber;
      set => tableNumber = value;
    }
    public float OrderWaitTime => orderWaitTime;
    public float FoodWaitTime => foodWaitTime;

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
      hasOrdered = true;
      order.SetActive(true);
      AddOrder();
    }

    private void AddOrder()
    {
      orders.order[tableNumber] = menu.order[randomOrder];
      orders.cooked[tableNumber] = false;
      orders.changedOrder = true;
    }

    public bool CheckForOrder()
    {
      if(itemHeld.value is not null)
      {
        if(itemHeld.value.tag == orderTag)
        {
          order.SetActive(false);
          Destroy(itemHeld.value);
          itemHeld.value = null;
          orders.order[tableNumber] = null;
          orders.changedOrder = true;
          customerMovement.goingToCashier = true;
          customerMovement.Invoke("MovePlayerToCashier", 3f);
          return true;
        }
      }
      return false;
    }

    public void CheckOutCustomer()
    {
      exiting = true;
      money.value += orderPrice;
      customerMovement.MovePlayerToExit();
    }

    public void LeaveWithoutPaying()
    {
      orders.order[tableNumber] = null;
      orders.changedOrder = true;
      order.SetActive(false);
      exiting = true;
      customerMovement.MovePlayerToExit();
    }
}
