using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private BoolObject gameTimer, ableToSleep;
    [SerializeField] private IntObject day;
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private float minSpawnTime, maxSpawnTime, spawnSpeed;
    [SerializeField] private UnityEvent onLastCustomerArrival;
    private bool canSpawnTwo;
    private int spawnAmount, currentSpawns;
    private float currentTime, spawnTime;
    private GameObject customerHolder;

    private void Start()
    {
      StartDay();
    }

    public void StartDay()
    {
      if(customerHolder is not null)
      {
        Destroy(customerHolder);
      }
      customerHolder = new GameObject();
      currentSpawns = 0;
      SelectSpawnAmount();
      SelectNewSpawnTime();
    }

    private void SelectSpawnAmount()
    {
      spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount);
    }

    public void EnableMultiple()
    {
      canSpawnTwo = true;
    }

    private void Update()
    {
      if(gameTimer.value && currentSpawns <= spawnAmount)
      {
        currentTime += Time.deltaTime * 2;
        if(currentTime >= spawnTime)
        {
          SpawnCustomers();
          SelectNewSpawnTime();
          currentTime = 0;
        }
      }
    }

    private void SelectNewSpawnTime()
    {
      spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void SpawnCustomers()
    {
      int tempRand = Random.Range(0, 1 + 1);
      if(tempRand == 0 || !canSpawnTwo)
      {
        Instantiate(customer, transform.position, customer.transform.rotation, customerHolder.transform);
        currentSpawns++;
      } else {
        GameObject spawnedCustomer = Instantiate(customer, new Vector2(transform.position.x + 1, transform.position.y), customer.transform.rotation, customerHolder.transform);
        Customer tempTempCustomer = spawnedCustomer.GetComponent<Customer>();
        spawnedCustomer = Instantiate(customer, transform.position, customer.transform.rotation, customerHolder.transform);
        Customer tempCustomer = spawnedCustomer.GetComponent<Customer>();
        tempCustomer.paired = true;
        tempTempCustomer.isPair = true;
        tempCustomer.pairedCustomer = tempTempCustomer;
        tempTempCustomer.pairedCustomer = tempCustomer;
        currentSpawns += 2;
      }
      if(currentSpawns >= spawnAmount)
      {
        onLastCustomerArrival.Invoke();
      }
    }

    public void CheckForAbleToSleep()
    {
      if(customerHolder.transform.childCount == 0 && currentSpawns >= spawnAmount)
      {
        ableToSleep.SetTrue();
      }
    }
}
