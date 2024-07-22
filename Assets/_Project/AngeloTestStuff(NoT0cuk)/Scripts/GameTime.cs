using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameTime : MonoBehaviour
{

    [SerializeField] private TMP_Text hoursText, minutesText;
    [SerializeField] private float gameSpeed;
    [SerializeField] private BoolObject gameTimer;
    [SerializeField] private UnityEvent dayEnd;
    private float timer;
    private int hours, minutes;

    private void Start()
    {
      ResetTime();
    }

    public void ResetTime()
    {
      hours = 9;
      minutes = 0;
      gameTimer.value = false;
      UpdateTime();
    }

    public void StartTimer()
    {
      gameTimer.value = true;
    }

    public void StopTimer()
    {
      gameTimer.value = false;
    }

    private void Update()
    {
      if(gameTimer.value)
      {
        Timer();
      }
    }

    private void Timer()
    {
      timer += Time.deltaTime;
      if(timer >= gameSpeed)
      {
        timer = 0;
        if(minutes == 45)
        {
          minutes = 00;
          hours++;
          CheckForEndOfDay();
          UpdateTime();
        } else {
          minutes += 1;
          UpdateTime();
        }
      }
    }

    private void UpdateTime()
    {
      if(hours < 10)
      {
        hoursText.text = "0" + hours.ToString();
      } else {
        hoursText.text = hours.ToString();
      }

      if(minutes < 10)
      {
        minutesText.text = "0" + minutes.ToString();
      } else {
        minutesText.text = minutes.ToString();
      }
    }

    private void CheckForEndOfDay()
    {
      if(hours == 21)
      {
        gameTimer.value = false;
        dayEnd.Invoke();
      }
    }
}
