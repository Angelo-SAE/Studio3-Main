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
    private bool menuActive;

    public GameObject FoodHolder => foodHolder;

    private void Update()
    {
      if(menuActive && !isCooking)
      {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          CloseCooker();
        }
      }
    }

    public override void Interact()
    {
      if(!paused.value)
      {
        onInteract.Invoke();
        menuActive = true;
      }
    }

    public override void AltInteract() {}

    public void CloseCooker()
    {
      menuActive = false;
      onSecondInteract.Invoke();
      Invoke("UnPauseGame", 0.1f);
    }

    private void UnPauseGame()
    {
      paused.SetFalse();
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
