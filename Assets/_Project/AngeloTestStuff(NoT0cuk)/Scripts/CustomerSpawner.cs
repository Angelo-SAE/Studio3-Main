using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private BoolObject gameTimer, ableToSleep;
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private float minSpawnTime, maxSpawnTime, spawnSpeed;
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

    private void Update()
    {
      if(gameTimer.value && currentSpawns != spawnAmount)
      {
        currentTime += Time.deltaTime * 2;
        if(currentTime >= spawnTime)
        {
          SpawnCustomers();
          SelectNewSpawnTime();
          currentSpawns++;
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
      Instantiate(customer, transform.position, customer.transform.rotation, customerHolder.transform);
    }

    public void CheckForAbleToSleep()
    {
      if(customerHolder.transform.childCount == 0 && currentSpawns == spawnAmount)
      {
        ableToSleep.SetTrue();
      }
    }
}
