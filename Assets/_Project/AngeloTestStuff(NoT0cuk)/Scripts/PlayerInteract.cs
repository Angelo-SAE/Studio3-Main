using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private GameObject detectionPosition, itemHolder;
    [SerializeField] private LayerMask pickUpItems, dropSpots, interactableObject;
    [SerializeField] private Animator playerAnimator;

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
      if(Input.GetKeyDown(KeyCode.E))
      {
        DetectItemPickUpOrDrop();
      }
      RotateDetection();
    }

    private void RotateDetection()
    {
      if(Input.GetKeyDown(KeyCode.W))
      {
        transform.rotation = Quaternion.Euler(0, 0, 180);
      }
      if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
      {
        transform.localRotation = Quaternion.Euler(0, 0, 90);
      }
      if(Input.GetKeyDown(KeyCode.S))
      {
        transform.rotation = Quaternion.Euler(0, 0, 0);
      }
    }

    private void DetectItemPickUpOrDrop()
    {
      if(Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, pickUpItems) && itemHeld.value is null)
      {
        GetPickUpItem();
      } else if(Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, dropSpots) && itemHeld.value is not null)
      {
        GetDropPosition();
      } else {
        Collider2D tempCollider = Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, interactableObject);
        if(tempCollider is not null)
        {
          tempCollider.gameObject.GetComponent<Interactable>().Interact();
        }
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
      UpdateCarryingAnimation(true);
      item.transform.SetParent(itemHolder.transform);
      item.transform.localPosition = Vector2.zero;
      itemHeld.value = item;
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
      UpdateCarryingAnimation(false);
      itemHeld.value.transform.SetParent(dropPosition.transform);
      itemHeld.value.transform.localScale = Vector2.one;
      itemHeld.value.transform.localPosition = Vector2.zero;
      itemHeld.value = null;
    }

    public void UpdateCarryingAnimation(bool set)
    {
      playerAnimator.SetBool("Carrying", set);
    }

    private float GetDistance(GameObject first, GameObject second)
    {
      return Mathf.Abs(first.transform.position.x - second.transform.position.x) + Mathf.Abs(first.transform.position.y - second.transform.position.y);
    }
}
