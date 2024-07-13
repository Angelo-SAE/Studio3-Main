using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : Interactable
{
    [SerializeField] private MenuObject menu;
    [SerializeField] private GameObject foodHolder, mush;
    [SerializeField] private UnityEvent onInteract;

    public GameObject FoodHolder => foodHolder;

    public override void Interact()
    {
      onInteract.Invoke();
    }

    public void CookOrder(int orderNumber)
    {
      Instantiate(menu.orderObjects[orderNumber], foodHolder.transform);
    }

    public void CookMush()
    {
      Instantiate(mush, foodHolder.transform);
    }
}
