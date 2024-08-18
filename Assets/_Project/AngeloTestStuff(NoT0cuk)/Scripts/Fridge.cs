using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : Interactable
{
    [SerializeField] private BoolObject paused;
    [SerializeField] private UnityEvent onInteract, onSecondInteract;
    private bool menuActive;

    private void Update()
    {
      if(menuActive)
      {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          CloseFridge();
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

    public void CloseFridge()
    {
      menuActive = false;
      onSecondInteract.Invoke();
      Invoke("UnPauseGame", 0.1f);
    }

    private void UnPauseGame()
    {
      paused.SetFalse();
    }

    public override void AltInteract() {}
}
