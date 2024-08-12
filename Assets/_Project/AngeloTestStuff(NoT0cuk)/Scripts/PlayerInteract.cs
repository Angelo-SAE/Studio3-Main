using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private BoolObject paused, gamePause;
    [SerializeField] private float detectionRadius;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private GameObject detectionPosition, itemHolder;
    [SerializeField] private LayerMask pickUpItems, dropSpots, interactableObject;
    [SerializeField] private PlayerMovement pMovement;

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(detectionPosition.transform.position, detectionRadius);
    }

    private void Awake()
    {
      itemHeld.value = null;
    }

    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.E) && !gamePause.value)
      {
        DetectItemPickUpOrDrop();
      }
      if(Input.GetKeyDown(KeyCode.F) && !gamePause.value)
      {
        InputAltInteract();
      }
    }

    public void RotateDetection(int direction)
    {
      switch(direction)
      {
        case(0):
        transform.rotation = Quaternion.Euler(0, 0, 180);
        break;
        case(1):
        transform.localRotation = Quaternion.Euler(0, 0, 90);
        break;
        case(2):
        transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      }
    }

    private void DetectItemPickUpOrDrop()
    {
      if(Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, pickUpItems))
      {
        if(itemHeld.value is null)
        {
          GetPickUpItem();
        }
      } else if(Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, dropSpots) && itemHeld.value is not null)
      {
        GetDropPosition();
      } else {
        Collider2D tempCollider = Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, interactableObject);
        if(tempCollider is not null)
        {
          tempCollider.gameObject.GetComponent<Interactable>().Interact();
          UpdateCarryingAnimation();
        }
      }
    }

    private void InputAltInteract()
    {
      Collider2D tempCollider = Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, interactableObject);
      if(tempCollider is not null)
      {
        tempCollider.gameObject.GetComponent<Interactable>().AltInteract();
        UpdateCarryingAnimation();
      }
    }

    private void GetPickUpItem()
    {
      Collider2D[] item = Physics2D.OverlapCircleAll(detectionPosition.transform.position, detectionRadius, pickUpItems);
      GameObject tempObject = null;
      if(item.Length == 1)
      {
        tempObject = item[0].gameObject;
      }
      for(int a = 0; a < item.Length - 1; a++)
      {
        if(GetDistance(detectionPosition,item[a].gameObject) < GetDistance(detectionPosition,item[a + 1].gameObject))
        {
          tempObject = item[a].gameObject;
        } else {
          tempObject = item[a + 1].gameObject;
        }
      }
      PickUpItem(tempObject);
      //Debug.Log(item);
      //Debug.Log(tempObject);
    }

    private void PickUpItem(GameObject item)
    {
      item.transform.SetParent(itemHolder.transform);
      item.transform.localPosition = Vector2.zero;
      itemHeld.value = item;
      itemHeld.value.GetComponent<Collider2D>().enabled = false;
      UpdateCarryingAnimation();
    }

    private void GetDropPosition()
    {
      Collider2D[] spots = Physics2D.OverlapCircleAll(detectionPosition.transform.position, detectionRadius, dropSpots);
      GameObject tempSpot = null;
      if(spots.Length == 1)
      {
        tempSpot = spots[0].gameObject;
      }
      for(int a = 0; a < spots.Length - 1; a++)
      {
        if(GetDistance(detectionPosition, spots[a].gameObject) < GetDistance(detectionPosition, spots[a + 1].gameObject))
        {
          tempSpot = spots[a].gameObject;
        } else {
          tempSpot = spots[a + 1].gameObject;
        }
      }
      DropItem(tempSpot.transform.GetChild(0).gameObject);
    }

    private void DropItem(GameObject dropPosition)
    {
      itemHeld.value.transform.SetParent(dropPosition.transform);
      itemHeld.value.transform.localScale = Vector2.one;
      itemHeld.value.transform.localPosition = Vector2.zero;
      itemHeld.value.GetComponent<Collider2D>().enabled = true;
      itemHeld.value = null;
      UpdateCarryingAnimation();
    }

    public void UpdateCarryingAnimation()
    {
      if(itemHeld.value is not null)
      {
        pMovement.carrying = true;
      } else {
        pMovement.carrying = false;
      }
    }

    private float GetDistance(GameObject first, GameObject second)
    {
      return Mathf.Abs(first.transform.position.x - second.transform.position.x) + Mathf.Abs(first.transform.position.y - second.transform.position.y);
    }
}
