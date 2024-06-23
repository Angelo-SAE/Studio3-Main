using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerMovement customerMovement;
    [SerializeField] private GameObject order;
    private bool hasOrdered;

    public bool HasOrdered => hasOrdered;

    public void MoveCustomerToPosition(Vector2Int position)
    {
      customerMovement.MoveToPosition(position);
    }

    public void DisplayOrder()
    {
      hasOrdered = true;
      order.SetActive(true);
    }
}
