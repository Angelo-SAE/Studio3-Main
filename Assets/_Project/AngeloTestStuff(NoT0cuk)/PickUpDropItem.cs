using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropItem : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private GameObjectObject itemHeld;
    [SerializeField] private GameObject detectionPosition, itemHolder;
    [SerializeField] private LayerMask pickUpItems, dropSpots;

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
    }

    private void DetectItemPickUpOrDrop()
    {
      if(Physics2D.OverlapCircle(detectionPosition.transform.position, detectionRadius, pickUpItems) && itemHeld.value is null)
      {
        GetPickUpItem();
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
    }

    private float GetDistance(GameObject first, GameObject second)
    {
      return Mathf.Abs(first.transform.position.x - second.transform.position.x) + Mathf.Abs(first.transform.position.y - second.transform.position.y);
    }
}
