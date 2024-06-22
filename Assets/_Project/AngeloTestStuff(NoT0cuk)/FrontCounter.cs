using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCounter : MonoBehaviour
{
    [SerializeField] private GameObjectObject frontCounter;
    [SerializeField] private Vector2Int[] waitingSpots;
    private LinkedList<GameObject> customers;
    private int currentSpot;

    private void Awake()
    {
      SetFrontCounter();
      customers = new LinkedList<GameObject>();
    }

    private void SetFrontCounter()
    {
      frontCounter.value = gameObject;
    }

    public Vector2Int GetNextSpot()
    {
      Vector2Int temp = waitingSpots[currentSpot];
      currentSpot++;
      return temp;
    }

    public void AddCustomer(GameObject customer)
    {
      customers.AddToBack(customer);
    }
}
