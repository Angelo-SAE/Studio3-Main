using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress : MonoBehaviour
{
    [SerializeField] private FloatObject stress;
    [SerializeField] private Slider stressSlider;
    [SerializeField] private BoolObject inRestaurant;
    [SerializeField] private GameObjectObject frontCounter, cashier;
    private FrontCounter counter1;
    private Cashier counter2;
    [SerializeField] private float stressIncreaseCounters;


    private void Start()
    {
      inRestaurant.value = false;
      stress.value = 0;
      UpdateSlider();
      counter1 = frontCounter.value.GetComponent<FrontCounter>();
      counter2 = cashier.value.GetComponent<Cashier>();
    }

    private void Update()
    {
      if(stressSlider.value != stress.value)
      {
        UpdateSlider();
      }
        /*if(inRestaurant.value)
        {
          if(counter1.Customers.Count() > 1 || counter2.Customers.Count() > 1)
          {
            stress.value += stressIncreaseCounters * Time.deltaTime;
          }
        }*/

      
      stress.value += stressIncreaseCounters * Time.deltaTime;


      stress.value = Mathf.Max(0, stress.value);
    }

    private void UpdateSlider()
    {
      if(stress.value > stressSlider.maxValue)
      {
        stress.value = stressSlider.maxValue;
      } else if(stress.value < stressSlider.minValue)
      {
        stress.value = stressSlider.minValue;
      }
      stressSlider.value = stress.value;
    }
}
