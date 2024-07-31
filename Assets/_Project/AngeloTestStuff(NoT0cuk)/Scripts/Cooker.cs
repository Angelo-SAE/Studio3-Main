using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : Interactable
{
    [SerializeField] private BoolObject paused;
    [SerializeField] private MenuObject menu;
    [SerializeField] private GameObject foodHolder, mush;
    [SerializeField] private UnityEvent onInteract, onSecondInteract;
    public bool isCooking;

    public GameObject FoodHolder => foodHolder;

    public override void Interact()
    {
      if(!paused.value)
      {
        onInteract.Invoke();
      } else if(!isCooking)
      {
        onSecondInteract.Invoke();
      }
    }

    public override void AltInteract() {}

    public void CookOrder(int orderNumber)
    {
      Instantiate(menu.orderObjects[orderNumber], foodHolder.transform);
    }

    public void CookMush()
    {
      Instantiate(mush, foodHolder.transform);
    }
}
