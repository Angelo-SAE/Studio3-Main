using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Interactable
{
    [SerializeField] private GameObjectObject itemHeld;

    public override void Interact()
    {
      if(itemHeld.value is not null)
      {
        Destroy(itemHeld.value);
        itemHeld.value = null;
      }
    }
}
