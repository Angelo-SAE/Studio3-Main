using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedGrandfather : Interactable
{
    [SerializeField] private GameObjectObject playerObject;
    [SerializeField] private OrderObject orders;
    [SerializeField] private PlayerMovement pMovement;
    [SerializeField] private UnityEvent afterFeed;

    public override void Interact()
    {
      FeedFood();
    }

    public override void AltInteract() {}

    private void FeedFood()
    {
      if(playerObject.value.tag == "Cheese Burger")
      {
        Destroy(playerObject.value);
        pMovement.carrying = false;
        WhatHappensNext();
      }
    }

    private void WhatHappensNext()
    {
      afterFeed.Invoke();
    }
}
