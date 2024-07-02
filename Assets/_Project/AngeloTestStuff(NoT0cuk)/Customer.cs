using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerMovement customerMovement;
    [SerializeField] private MenuObject menu;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private FloatObject money;
    [SerializeField] private GameObject order, orderSprite;
    private string orderTag;
    private float orderPrice;
    private bool hasOrdered;

    public bool HasOrdered => hasOrdered;

    private void Start()
    {
      ChooseOrder();
    }

    private void ChooseOrder()
    {
      int randomOrder = Random.Range(0, menu.orderTag.Length);
      orderTag = menu.orderTag[randomOrder];
      orderPrice = menu.orderPrice[randomOrder];
      Instantiate(menu.orderObject[randomOrder], orderSprite.transform);
    }

    public void MoveCustomerToPosition(Vector2Int position)
    {
      customerMovement.MoveToPosition(position);
    }

    public void DisplayOrder()
    {
      hasOrdered = true;
      order.SetActive(true);
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
