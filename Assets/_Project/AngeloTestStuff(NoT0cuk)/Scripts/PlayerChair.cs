using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChair : Interactable
{
    [SerializeField] private FloatObject stress;
    [SerializeField] private BoolObject paused;
    [SerializeField] private GameObjectObject player;
    [SerializeField] private Vector2 chairPosition, exitPositionRight, exitPositionDown, exitPositionLeft;
    [SerializeField] private float stressReduction;
    public bool playerInChair;

    public override void Interact()
    {
      if(!paused.value)
      {
        paused.SetTrue();
        AddPlayerToChair();
      }
    }

    public override void AltInteract() {}

    private void Update()
    {
      if(playerInChair)
      {
        stress.value -= stressReduction * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.D))
        {
          RemovePlayerFromChair(0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
          RemovePlayerFromChair(1);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
          RemovePlayerFromChair(2);
        }
      }
    }

    private void AddPlayerToChair()
    {
      player.value.GetComponent<Collider2D>().enabled = false;
      player.value.transform.position = chairPosition;
      playerInChair = true;
    }

    private void RemovePlayerFromChair(int direction)
    {
      switch(direction)
      {
        case(0):
        player.value.transform.position = exitPositionRight;
        break;
        case(1):
        player.value.transform.position = exitPositionDown;
        break;
        case(2):
        player.value.transform.position = exitPositionLeft;
        break;
      }

      player.value.GetComponent<Collider2D>().enabled = true;
      paused.SetFalse();
      playerInChair = false;
    }



}
