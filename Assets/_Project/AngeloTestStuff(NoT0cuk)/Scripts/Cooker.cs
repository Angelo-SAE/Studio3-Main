using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : Interactable
{
    [SerializeField] private MenuObject menu;
    [SerializeField] private GameObject foodHolder;
    [SerializeField] private UnityEvent onInteract;

    public override void Interact()
    {
      onInteract.Invoke();
    }

    public void CookOrder(int orderNumber)
    {
      Instantiate(menu.orderObjects[orderNumber], foodHolder.transform);
    }
}
