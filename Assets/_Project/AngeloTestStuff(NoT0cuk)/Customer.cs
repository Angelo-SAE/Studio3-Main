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
    private int randomOrder, tableNumber;
    private string orderTag;
    private float orderPrice;
    private bool hasOrdered;

    public bool HasOrdered => hasOrdered;

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

    public void DisplayOrder(int table)
    {
      hasOrdered = true;
      order.SetActive(true);
      tableNumber = table;
      AddOrder();
    }

    private void AddOrder()
    {
      orders.order[tableNumber] = menu.order[randomOrder];
      orders.addedOrder = true;
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
          customerMovement.MovePlayerToCashier();
          return true;
        }
      }
      return false;
    }

    public void CheckOutCustomer()
    {
      money.value += orderPrice;
      Destroy(gameObject);
    }
}
